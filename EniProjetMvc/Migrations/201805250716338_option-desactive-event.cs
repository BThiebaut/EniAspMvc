namespace EniProjetMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class optiondesactiveevent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Themes", "Desactive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Themes", "Desactive");
        }
    }
}
