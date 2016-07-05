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
            if (DateTime.Today.Date == LastUpdate.Date)
                return;

            // get the ten latest decisions from the epo server
            try
            {
                List<Decision> dlist = await EpoSearch.SearchLatestAsync();
                LastUpdate = DateTime.Today.Date;
                if (dlist == null || dlist.Count == 0)
                    return;

                DecisionDbContext dbContext = new DecisionDbContext();

                foreach (Decision decision in dlist)
                    dbContext.AddOrUpdate(decision);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong is checking for decisions in the DB or in writing a new decision to it. Exception: " + e.Message + " Inner exception: " + e.InnerException);
            }
        }



    }
}