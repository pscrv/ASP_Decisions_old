using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASP_Decisions.Epo_facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP_Decisions.Models;

namespace ASP_Decisions.Epo_facade.Tests
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
}