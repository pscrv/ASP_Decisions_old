using ASP_Decisions.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASP_Decisions.Epo_facade
{
    public static class EpoSearch
    {
        #region public search methods
        public static Task<List<Decision>> SearchCaseNumberAsync(string caseNumber)
        {
            return SearchAsync("", "", "dg3CSNCase:" + caseNumber, 0, 1000);
        }

        public static Task<List<Decision>> SearchLatestAsync(int number = 10)
        {
            return SearchByDateAsync(DateTime.Parse ("1/1/1900"),  DateTime.Today, number);
        }

        public static Task<List<Decision>> SearchByDateAsync(DateTime startDate, DateTime endDate, int number = 1000)
        {
            //date format: 2016-07-01
            string startString = startDate.ToString("yyyy-MM-dd");
            string endString = endDate.ToString("yyyy-MM-yy");
            string queryString = "inmeta:dg3DecisionDate:" + startString  + ".." + endString;
            return SearchAsync(queryString, "", "", 0, number);
        }

        public static Task<List<Decision>> SearchByBoardAsync(string board, int number = 1000)
        {
            return SearchAsync("", "dg3BOAnDot:" + board, "", 0, number);
        }

        public static async Task<List<Decision>> SearchAsync(string query, string required, string partial, int start, int number)
        {
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            queryString["q"] = query;
            queryString["requiredfields"] = required;
            queryString["partialfields"] = partial;
            queryString["getfields"] = "*";
            queryString["start"] = start.ToString();
            queryString["num"] = number.ToString();
            queryString["filter"] = "0";
            queryString["site"] = "BoA";
            queryString["client"] = "BoA_AJAX";
            queryString["ie"] = "latin1";
            queryString["oe"] = "latin1";
            queryString["entsp"] = "0";
            queryString["sort"] = "date:D:R:d1";
            
            UriBuilder uriBuilder = new UriBuilder("http://www.epo.org/footer/search.html");
            uriBuilder.Query = queryString.ToString();
            Uri uri = uriBuilder.Uri;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync(uri).ConfigureAwait(false);
                GSP gsp = new GSP();
                try
                {
                    List<Decision> decList = new List<Decision>();
                    gsp = await _parseXML(res);
                    if (gsp == null || gsp.RES == null || gsp.RES.R == null || gsp.RES.R.Length == 0)
                        return decList;

                    foreach (GSPRESR result in gsp.RES.R)
                        decList.Add(EPOSearchResultToDecision(result));
                    return decList;
                }
                finally
                {
                    gsp.Dispose();
                }
            }

        }
        #endregion


        #region html methods
        private static async Task<HtmlDocument> _docmentFromLinkAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync(url).ConfigureAwait(false);
                HtmlDocument doc = new HtmlDocument();
                string charset = res.Content.Headers.ContentType.CharSet;
                if (charset == null)
                    charset = "utf-8";
                Encoding encoding = Encoding.GetEncoding(charset);

                using (Stream sr = await res.Content.ReadAsStreamAsync())
                using (StreamReader sre = new StreamReader(sr, encoding))
                {
                    doc.Load(sre);
                    return doc;
                }
            }            
        }
        
        public static void GetDecisionText(Decision decision, bool forceUpdate = false)
        {
            if (decision.TextDownloaded && !forceUpdate)
                return;

            if (!decision.MetaDownloaded || decision.Link == "")
                return;

            HtmlDocument htmldoc = _docmentFromLinkAsync(decision.Link).Result;

            HtmlNode bodyDiv = htmldoc.DocumentNode.SelectNodes("//div[@id='body']")[0];
            if (bodyDiv == null)
                return;

            List<HtmlNode> toRemove = new List<HtmlNode>();
            foreach (HtmlNode node in bodyDiv.ChildNodes)
            {
                if (node.Name != "p")
                    toRemove.Add(node);
            }

            foreach (HtmlNode node in toRemove)
                node.Remove();

            IList<HtmlNode> headers = bodyDiv.SelectNodes("//b");
            if (headers.Count != 3)
            {
                decision.FactsHeader = "See Reasons.";
                decision.Facts = "";
                decision.ReasonsHeader = "";
                decision.Reasons = _stringFromParas(bodyDiv.SelectNodes("//p"));
                decision.OrderHeader = "See Reasons.";
                decision.Order = "";
                decision.TextDownloaded = true;
                decision.HasSplitText = false;                
            }
            else
            {
                decision.FactsHeader = headers[0].InnerText;
                decision.Facts = _stringFromParas(_loopToHeader(headers[0].ParentNode.NextSibling));
                decision.ReasonsHeader = headers[1].InnerText;
                decision.Reasons = _stringFromParas(_loopToHeader(headers[1].ParentNode.NextSibling));
                decision.OrderHeader = headers[2].InnerText;
                decision.Order = _stringFromParas(_loopToHeader(headers[2].ParentNode.NextSibling));
                decision.TextDownloaded = true;
                decision.HasSplitText = true;
            } 
        }

        private static string _stringFromParas(IList<HtmlNode> paras)
        {
            StringBuilder builder = new StringBuilder();
            foreach (HtmlNode p in paras)
            {
                if (!_isPunctuationAndWhitespace(p.InnerText))
                {
                    builder.Append(p.InnerText.Trim());
                    builder.Append("\n\n");
                }
            }            
            return builder.ToString();
        }

        private static List<HtmlNode> _loopToHeader(HtmlNode node)
        {
            List<HtmlNode> paraList = new List<HtmlNode>();
            while (node != null)
            {
                paraList.Add(node);
                node = node.NextSibling;
                if (node != null)
                {
                    HtmlNode nextSib = node.NextSibling;
                    if (nextSib == null || nextSib.Element("b") != null)
                    // there is no next sibling, or the next sibling is a header
                    // either way, we have reached the end of the section
                    // so add this node and return
                    {
                        paraList.Add(node);
                        return paraList;
                    }
                    // else: keep looping
                }
                else
                    return paraList; // we have just dealt with the last node
            }
            return null; // only if the node passed in is null?
        }  
        #endregion


        #region private helper methods
        private static async Task<GSP> _parseXML(HttpResponseMessage response)
        {
            using (Stream sr = await response.Content.ReadAsStreamAsync())
            using (StreamReader sre = new StreamReader(sr, Encoding.Default))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GSP));
                return (GSP)serializer.Deserialize(sre);
            }
        }

        private static Decision EPOSearchResultToDecision(GSPRESR result)
        {
            NameValueCollection nvc = new NameValueCollection();
            foreach (GSPRESRMT metafield in result.MT)
                nvc[metafield.N] = metafield.V;

            Decision decision = new Decision();
            decision.Appellants         = "Must implement some extraction for this.";
            decision.Applicant          = _formatString(nvc["dg3Applicant"]);
            decision.ApplicationNumber  = _formatString(nvc["dg3APN"]);
            decision.Articles           = _formatString(nvc["dg3ArtRef"]);
            decision.Board              = _formatString(nvc["dg3DecisionBoard"]);
            decision.CaseNumber         = _formatString(nvc["dg3CSNCase"]);
            decision.Catchwords         = "Must implement some extraction for this.";
            decision.CitedCases         = _formatString(nvc["dg3aDCI"]);
            decision.DecisionDate       = DateTime.Parse(nvc["dg3DecisionDate"]);
            decision.DecisionLanguage   = Generic.LanguagesDictionary[nvc["dg3DecisionLang"].ToUpper()];                ;
            decision.Distribution       = Generic.DistributionDictionary[nvc["dg3DecisionDistributionKey"].ToUpper()];
            decision.Ecli               = _formatString(nvc["dg3ECLI"]);
            decision.Ipc                = _formatString(nvc["dg3CaseIPC"]);
            decision.Keywords           = _formatString(nvc["dg3KEY"]);
            decision.OnlineDate         = DateTime.Parse(nvc["dg3DecisionOnline"]);
            decision.Opponents          = _formatString(nvc["dg3Opponent"]);
            decision.ProcedureLanguage  = Generic.LanguagesDictionary[nvc["dg3DecisionPRL"].ToUpper()];
            decision.Respondents        = "Must implement some extraction for this.";
            decision.Title              = _formatString(nvc["dg3TLE"]);

            decision.Link               = _formatString(result.U);

            Regex rgx = new Regex(@"http.*?pdf", RegexOptions.IgnoreCase);
            Match m = rgx.Match(nvc["dg3DecisionPDF"]);
            if (m.Success)
                decision.PdfLink = _formatString(m.Value);
            else
                decision.PdfLink = "";

            rgx = new Regex(@"\((.*)\)");
            decision.Headword = _formatString(rgx.Match(nvc["DC.Title"]).ToString());  

            decision.MetaDownloaded = true;
            return decision;
        }

        private static bool _isPunctuationAndWhitespace(string value)
        {
            bool result = true;
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsPunctuation(value[i]) && !char.IsWhiteSpace(value[i]))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private static string _formatString(string str)
        {
            if (_isPunctuationAndWhitespace(str))
                return "";
            else
                return str.Trim();
        }
        #endregion
        















    }
}