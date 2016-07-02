using ASP_Decisions.Models;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASP_Decisions.Epo_facade
{
    public static class EpoSearch
    {
        #region public search methods
        public static Task<GSP> SearchCaseNumberAsync(string caseNumber)
        {
            return SearchAsync("", "", "dg3CSNCase:" + caseNumber, 0, 1000);
        }

        public static Task<GSP> SearchLatestAsync(int number = 10)
        {
            return SearchByDateAsync(DateTime.Parse ("1/1/1900"),  DateTime.Today, number);
        }

        public static Task<GSP> SearchByDateAsync(DateTime startDate, DateTime endDate, int number = 1000)
        {
            //date format: 2016-07-01
            string startString = startDate.ToString("yyyy-MM-dd");
            string endString = endDate.ToString("yyyy-MM-yy");
            string queryString = "inmeta:dg3DecisionDate:" + startString  + ".." + endString;
            return SearchAsync(queryString, "", "", 0, number);
        }

        public static Task<GSP> SearchByBoardAsync(string board, int number = 1000)
        {
            return SearchAsync("", "dg3BOAnDot:" + board, "", 0, number);
        }

        public static async Task<GSP> SearchAsync(string query, string required, string partial, int start, int number)
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
                HttpResponseMessage res = await client.GetAsync(uri);
                return await _parseXML(res);
            }

        }
        #endregion


        #region private helper methods
        private static async Task<GSP> _parseXML(HttpResponseMessage response)
        {
            using (Stream sr = await response.Content.ReadAsStreamAsync())
            using (StreamReader sre = new StreamReader(sr))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GSP));
                return (GSP)serializer.Deserialize(sre);
            }
        }
        #endregion


        #region exract Decision data from GSP
        public static Decision EPOSearchResultToDecision(GSPRESR result)
        {
            NameValueCollection nvc = new NameValueCollection();
            foreach (GSPRESRMT metafield in result.MT)
                nvc[metafield.N] = metafield.V;

            Decision decision = new Decision();
            decision.Appellants         = "Must implement some extraction for this.";
            decision.Applicant          = nvc["dg3Applicant"];
            decision.ApplicationNumber  = nvc["dg3APN"];
            decision.Articles           = nvc["dg3ArtRef"];
            decision.Board              = nvc["dg3DecisionBoard"];
            decision.CaseNumber         = nvc["dg3CSNCase"];
            decision.Catchwords         = nvc["Must implement some extraction for this."];
            //decision.CitedCases       = nvc["dg3aDCI"];
            decision.DecisionDate       = DateTime.Parse(nvc["dg3DecisionDate"]);
            //decision.DecisionLanguage = nvc["dg3DecisionLang"];
            //decision.Distribution     = nvc["dg3DecisionDistributionKey"];
            decision.Ecli               = nvc["dg3ECLI"];
            //decision.Headword         = nvc["DC.Title"];
            decision.Ipc                = nvc["dg3CaseIPC"];
            decision.Keywords           = nvc["dg3KEY"];
            //decision.Link               = "";
            decision.OnlineDate         = DateTime.Parse(nvc["dg3DecisionOnline"]);
            decision.Opponents          = nvc["dg3Opponent"];
            //decision.PdfLink            = new Uri(nvc["dg3DecisionPDF"]);
            //decision.ProcedureLanguage = nvc["dg3DecisionPRL"];
            decision.Respondents        = "Must implement some extraction for this.";
            decision.Title              = nvc["dg3TLE"];

            decision.MetaDownloaed = true;


            return decision;
        }






        #endregion















    }
}