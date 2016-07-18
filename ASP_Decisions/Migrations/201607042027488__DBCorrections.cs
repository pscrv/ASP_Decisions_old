namespace ASP_Decisions_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _DBCorrections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Decisions", "FactsHeader", c => c.String());
            AlterColumn("dbo.Decisions", "Board", c => c.String(maxLength: 10));
            AlterColumn("dbo.Decisions", "Facts", c => c.String(unicode: false, storeType: "text"));
            DropColumn("dbo.Decisions", "FactHeader");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Decisions", "FactHeader", c => c.String());
            AlterColumn("dbo.Decisions", "Facts", c => c.String());
            AlterColumn("dbo.Decisions", "Board", c => c.String());
            DropColumn("dbo.Decisions", "FactsHeader");
        }
    }
}
