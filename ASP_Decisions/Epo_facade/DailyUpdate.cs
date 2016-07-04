using ASP_Decisions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Decisions.Epo_facade
{
    public static class DailyUpdate
    {
        public static DateTime LastUpdate { get; private set; } = DateTime.MinValue.Date;

        public static async Task TryUpdate()
        {
            // maximum of one update per day
            if (DateTime.Today.Date == LastUpdate)
                return;

            // get the ten latest decisions from the epo server
            try
            {
                List<Decision> dlist = await EpoSearch.SearchLatestAsync();
                LastUpdate = DateTime.Today.Date;
                if (dlist == null || dlist.Count == 0)
                    return;

                DecisionDbContext dbContext = new DecisionDbContext();
                var decs = dbContext.Decisions;
                
                foreach (Decision decision in dlist)
                {
                    Decision inDB = dbContext.Decisions.FirstOrDefault(
                        dec => dec.CaseNumber == decision.CaseNumber 
                                && dec.DecisionLanguage == decision.DecisionLanguage);

                    if (inDB == null)
                        dbContext.Decisions.Add(decision);
                }
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong is checking for decisions in the DB or in writing a new decision to it.");
            }
        }



    }
}