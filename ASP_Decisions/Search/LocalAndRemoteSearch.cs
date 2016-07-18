using ASP_Decisions_v1.Epo_facade;
using ASP_Decisions_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ASP_Decisions_v1.Search
{
    public static class LocalAndRemoteSearch
    {
        private static DecisionDbContext _dbContext = new DecisionDbContext();

        public static Decision SearchCaseNumber(string cn)
        {
            // returns a case from the DB, if there one.
            // either:
            // - the case with DecisionLanguage == ProcedureLanguage
            // - or the first found
            // if no case in the DB, queries the EPO DBs, and creates
            // a Decision for each language version found, then returns
            // as above

            if (cn.Trim().Length < 4) // too short to be real
                return null;

            cn = FormatCaseNumber(cn);

            
            Decision decision;
            IQueryable<Decision> inDB = from d in _dbContext.Decisions
                                        where d.CaseNumber == cn
                                        select d;

            if (inDB.Count() == 0)
            {
                List<Decision> dList = EpoSearch.SearchCaseNumberAsync(cn).Result;
                if (dList == null || dList.Count == 0)
                    return null;

                foreach (Decision d in dList)
                    _dbContext.AddOrUpdate(d);

                decision = dList.FirstOrDefault(
                    dec => dec.DecisionLanguage == dec.ProcedureLanguage);

                if (decision == null)
                    decision = dList.First();
            }
            else
            {
                IQueryable<Decision> dec = from d in inDB
                                           where d.DecisionLanguage == d.ProcedureLanguage
                                           select d;

                if (dec.Count() != 0)
                    decision = dec.First();
                else
                    decision = inDB.First();
            }

            return decision;
        }

        public static string FormatCaseNumber(string cn)
        {
            string search = cn.Trim().ToUpper();
            if (search == "")
                return cn;  // nothing there, send it back


            Regex re = new Regex(@"(.*)([DGJRTW]) *(\d*)/(\d*)(.*)");
            Match found = re.Match(search);
            if (!found.Success)
                return cn;

            if (found.Groups[1].Value != "" || found.Groups[5].Value != "")
                return cn;

            StringBuilder builder = new StringBuilder();
            builder.Append(found.Groups[2].Value);
            builder.Append(' ');
            builder.Append(found.Groups[3].Value.PadLeft(4, '0'));
            builder.Append('/');
            builder.Append(found.Groups[4].Value);

            return builder.ToString();
        }

    }
}