namespace EniProjetMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Utilisateur_evenement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Utilisateurs", "Evenement_Id", "dbo.Evenements");
            DropIndex("dbo.Utilisateurs", new[] { "Evenement_Id" });
            CreateTable(
                "dbo.UtilisateurEvenements",
                c => new
                    {
                        Utilisateur_Id = c.Int(nullable: false),
                        Evenement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Utilisateur_Id, t.Evenement_Id })
                .ForeignKey("dbo.Utilisateurs", t => t.Utilisateur_Id, cascadeDelete: true)
                .ForeignKey("dbo.Evenements", t => t.Evenement_Id, cascadeDelete: true)
                .Index(t => t.Utilisateur_Id)
                .Index(t => t.Evenement_Id);
            
            DropColumn("dbo.Utilisateurs", "Evenement_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Utilisateurs", "Evenement_Id", c => c.Int());
            DropForeignKey("dbo.UtilisateurEvenements", "Evenement_Id", "dbo.Evenements");
            DropForeignKey("dbo.UtilisateurEvenements", "Utilisateur_Id", "dbo.Utilisateurs");
            DropIndex("dbo.UtilisateurEvenements", new[] { "Evenement_Id" });
            DropIndex("dbo.UtilisateurEvenements", new[] { "Utilisateur_Id" });
            DropTable("dbo.UtilisateurEvenements");
            CreateIndex("dbo.Utilisateurs", "Evenement_Id");
            AddForeignKey("dbo.Utilisateurs", "Evenement_Id", "dbo.Evenements", "Id");
        }
    }
}
