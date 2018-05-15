namespace EniProjetMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class typeHeure : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Evenements", "HeureOuverture", c => c.String());
            AlterColumn("dbo.Evenements", "HeureFermeture", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Evenements", "HeureFermeture", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Evenements", "HeureOuverture", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
