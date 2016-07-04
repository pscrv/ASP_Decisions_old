namespace ASP_Decisions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _DateTimeModified : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Decisions", "DecisionDate", c => c.DateTime());
            AlterColumn("dbo.Decisions", "OnlineDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Decisions", "OnlineDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Decisions", "DecisionDate", c => c.DateTime(nullable: false));
        }
    }
}
