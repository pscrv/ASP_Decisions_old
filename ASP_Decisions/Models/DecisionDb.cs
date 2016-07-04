using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ASP_Decisions.Models
{
    public static class Generic
    {
        public enum Languages { DE, EN, FR }
        public enum DistributionCodes { A, B, C, D, Unknown }

        public static Dictionary<string, Languages> LanguagesDictionary
            = new Dictionary<string, Languages>
        {
                {"DE", Languages.DE },
                {"EN", Languages.EN },
                {"FR", Languages.FR },
        };

        public static Dictionary<string, DistributionCodes> DistributionDictionary
            = new Dictionary<string, DistributionCodes>
        {
                {"A", DistributionCodes.A },
                {"B", DistributionCodes.B },
                {"C", DistributionCodes.C},
                {"D", DistributionCodes.D },
                {"", DistributionCodes.Unknown }
        };
    }



    public class Decision
    {
        public int Id { get; set; }
        
        public string CaseNumber { get; set; }

        public bool MetaDownloaded { get; set; } = false;

        public DateTime? DecisionDate { get; set; } = null;
        public DateTime? OnlineDate { get; set; } = null;

        public string Applicant { get; set; } = "";
        public string Opponents { get; set; } = "";
        public string Appellants { get; set; } = "";
        public string Respondents { get; set; } = "";

        public string ApplicationNumber { get; set; } = "";
        public string Ipc { get; set; } = "";
        public string Title { get; set; } = "";
        public Generic.Languages? ProcedureLanguage { get; set; } = null;

        public string Board { get; set; } = "";
        public string Keywords { get; set; } = "";
        public string Articles { get; set; } = "";
        public string Rules { get; set; } = "";
        public string Ecli { get; set; } = "";

        public string CitedCases { get; set; } = "";
        public Generic.DistributionCodes? Distribution { get; set; } = null;
        public string Headword { get; set; } = "";
        public string Catchwords { get; set; } = "";

        public Generic.Languages? DecisionLanguage { get; set; } = null;

        public string Link { get; set; } = "";
        public string PdfLink { get; set; } = "";

        public bool TextDownloaded { get; set; } = false;
        public bool HasSplitText { get; set; } = false;
        public string FactHeader { get; set; } = "";
        public string ReasonsHeader { get; set; } = "";
        public string OrderHeader { get; set; } = "";
        public string Facts { get; set; } = "";
        public string Reasons { get; set; } = "";
        public string Order { get; set; } = "";
    }


    public class DecisionDbContext : DbContext
    {
        public DbSet<Decision> Decisions { get; set; }
    }
}
