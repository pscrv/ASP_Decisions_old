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
            GSP gsp = new GSP();
            try
            {
                gsp = await EpoSearch.SearchCaseNumberAsync("T 0641/00");
                GSPRESR res = gsp.RES.R[0];
                Decision dec = EpoSearch.EPOSearchResultToDecision(res);

                Assert.AreEqual(dec.CaseNumber, "T 0641/00");
            }
            finally
            {
                gsp.Dispose();
            }
        }
    }


    [TestClass()]
    public class EpoSearchTests
    {
        [TestMethod()]
        public async Task SearchTest()
        {
            GSP gsp = new GSP();
            try
            {
                gsp = await EpoSearch.SearchLatestAsync();
                Assert.AreEqual(gsp.RES.R.Count(), 10);

                gsp = await EpoSearch.SearchByBoardAsync("3501", 15);
                Assert.AreEqual(gsp.RES.R.Count(), 15);

                gsp = await EpoSearch.SearchCaseNumberAsync("T 0641/00");
                Assert.AreEqual(gsp.RES.R.Count(), 4);
            }
            finally
            {
                gsp.Dispose();
            }
        }
    }
}