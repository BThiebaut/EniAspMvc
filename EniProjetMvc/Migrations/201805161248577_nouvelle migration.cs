namespace EniProjetMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nouvellemigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Evenement_Id", "dbo.Evenements");
            DropForeignKey("dbo.Evenements", "Theme_Id", "dbo.Themes");
            DropIndex("dbo.Evenements", new[] { "Theme_Id" });
            DropIndex("dbo.Images", new[] { "Evenement_Id" });
            AlterColumn("dbo.Evenements", "Intitule", c => c.String(nullable: false));
            AlterColumn("dbo.Evenements", "HeureOuverture", c => c.String(nullable: false));
            AlterColumn("dbo.Evenements", "HeureFermeture", c => c.String(nullable: false));
            AlterColumn("dbo.Evenements", "Adresse", c => c.String(nullable: false));
            AlterColumn("dbo.Evenements", "Theme_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Utilisateurs", "Adresse", c => c.String(nullable: false));
            AlterColumn("dbo.Images", "Url", c => c.String(nullable: false));
            AlterColumn("dbo.Images", "Evenement_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Themes", "Libelle", c => c.String(nullable: false));
            CreateIndex("dbo.Evenements", "Theme_Id");
            CreateIndex("dbo.Images", "Evenement_Id");
            AddForeignKey("dbo.Images", "Evenement_Id", "dbo.Evenements", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Evenements", "Theme_Id", "dbo.Themes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evenements", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.Images", "Evenement_Id", "dbo.Evenements");
            DropIndex("dbo.Images", new[] { "Evenement_Id" });
            DropIndex("dbo.Evenements", new[] { "Theme_Id" });
            AlterColumn("dbo.Themes", "Libelle", c => c.String());
            AlterColumn("dbo.Images", "Evenement_Id", c => c.Int());
            AlterColumn("dbo.Images", "Url", c => c.String());
            AlterColumn("dbo.Utilisateurs", "Adresse", c => c.String());
            AlterColumn("dbo.Evenements", "Theme_Id", c => c.Int());
            AlterColumn("dbo.Evenements", "Adresse", c => c.String());
            AlterColumn("dbo.Evenements", "HeureFermeture", c => c.String());
            AlterColumn("dbo.Evenements", "HeureOuverture", c => c.String());
            AlterColumn("dbo.Evenements", "Intitule", c => c.String());
            CreateIndex("dbo.Images", "Evenement_Id");
            CreateIndex("dbo.Evenements", "Theme_Id");
            AddForeignKey("dbo.Evenements", "Theme_Id", "dbo.Themes", "Id");
            AddForeignKey("dbo.Images", "Evenement_Id", "dbo.Evenements", "Id");
        }
    }
}
