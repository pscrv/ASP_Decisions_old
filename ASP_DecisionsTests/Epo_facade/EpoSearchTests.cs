using ASP_Decisions_v1.Models;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Decisions_v1.Epo_facade.Tests
{
    [TestClass()]
    public class EpoExtractionTests
    {
        [TestMethod()]
        public async Task EPOSearchResultToDecisionTest()
        {
            List<Decision> dlist = await EpoSearch.SearchCaseNumberAsync("T 0641/00");
            Assert.AreEqual(dlist.First().CaseNumber, "T 0641/00");           
        }
    }


    [TestClass()]
    public class EpoSearchTests
    {
        [TestMethod()]
        public async Task SearchTest()
        {
            List<Decision> dlist = new List<Decision>();

            dlist = await EpoSearch.SearchLatestAsync();
            Assert.AreEqual(dlist.Count, 10);

            dlist = await EpoSearch.SearchByBoardAsync("3501", 15);
            Assert.AreEqual(dlist.Count, 15);

            dlist = await EpoSearch.SearchCaseNumberAsync("T 0641/00");
            Assert.AreEqual(dlist.Count, 4);

        }
    }

    [TestClass()]
    public class DailyUpdateTests
    {
        [TestMethod()]
        public async Task DailyUpdateTest()
        {
            await DailyUpdate.TryUpdate();
            Assert.AreEqual(DailyUpdate.LastUpdate.Date, DateTime.Today.Date);
        }
    }

    [TestClass()]
    public class FromHTMLTests
    {
        private string url = "http://www.epo.org/law-practice/case-law-appeals/recent/t000641ex1.html";
        private string url2 = "http://www.epo.org/law-practice/case-law-appeals/recent/t111317du1.html";

        //[TestMethod()]
        //public async Task DocmentFromLinkTest()
        //{
        //    HtmlDocument doc = await EpoSearch.DocmentFromLinkAsync(url);
        //    Assert.IsTrue(doc.DocumentNode != null);
        //}

        [TestMethod()]
        public void GetDecisionTextTest()
        {
            Decision dec = new Decision();
            dec.Link = url2;
            dec.MetaDownloaded = true;

            EpoSearch.GetDecisionText(dec);
            Assert.IsTrue(dec.TextDownloaded);
            Assert.IsTrue(dec.HasSplitText);
        }
    }
}