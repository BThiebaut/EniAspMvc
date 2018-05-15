namespace EniProjetMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class typeDT : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Evenements", "DateDebut", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Evenements", "DateFin", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Evenements", "HeureOuverture", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Evenements", "HeureFermeture", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Evenements", "HeureFermeture", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Evenements", "HeureOuverture", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Evenements", "DateFin", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Evenements", "DateDebut", c => c.DateTime(nullable: false));
        }
    }
}
