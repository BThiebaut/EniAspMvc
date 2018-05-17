namespace EniProjetMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr_img : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Evenement_Id", "dbo.Evenements");
            DropIndex("dbo.Images", new[] { "Evenement_Id" });
            AlterColumn("dbo.Images", "Evenement_Id", c => c.Int());
            CreateIndex("dbo.Images", "Evenement_Id");
            AddForeignKey("dbo.Images", "Evenement_Id", "dbo.Evenements", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Evenement_Id", "dbo.Evenements");
            DropIndex("dbo.Images", new[] { "Evenement_Id" });
            AlterColumn("dbo.Images", "Evenement_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Images", "Evenement_Id");
            AddForeignKey("dbo.Images", "Evenement_Id", "dbo.Evenements", "Id", cascadeDelete: true);
        }
    }
}
