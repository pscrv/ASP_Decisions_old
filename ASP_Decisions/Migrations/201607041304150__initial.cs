namespace ASP_Decisions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Decisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseNumber = c.String(),
                        MetaDownloaded = c.Boolean(nullable: false),
                        DecisionDate = c.DateTime(nullable: false),
                        OnlineDate = c.DateTime(nullable: false),
                        Applicant = c.String(),
                        Opponents = c.String(),
                        Appellants = c.String(),
                        Respondents = c.String(),
                        ApplicationNumber = c.String(),
                        Ipc = c.String(),
                        Title = c.String(),
                        ProcedureLanguage = c.Int(),
                        Board = c.String(),
                        Keywords = c.String(),
                        Articles = c.String(),
                        Rules = c.String(),
                        Ecli = c.String(),
                        CitedCases = c.String(),
                        Distribution = c.Int(),
                        Headword = c.String(),
                        Catchwords = c.String(),
                        DecisionLanguage = c.Int(),
                        Link = c.String(),
                        PdfLink = c.String(),
                        TextDownloaded = c.Boolean(nullable: false),
                        HasSplitText = c.Boolean(nullable: false),
                        FactHeader = c.String(),
                        ReasonsHeader = c.String(),
                        OrderHeader = c.String(),
                        Facts = c.String(),
                        Reasons = c.String(),
                        Order = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Decisions");
        }
    }
}
