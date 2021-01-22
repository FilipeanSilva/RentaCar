namespace PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Preco : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Veiculoes", "PrecoDia", c => c.Double(nullable: false));
            AddColumn("dbo.Veiculoes", "Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Veiculoes", "Id");
            AddForeignKey("dbo.Veiculoes", "Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Veiculoes", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Veiculoes", new[] { "Id" });
            DropColumn("dbo.Veiculoes", "Id");
            DropColumn("dbo.Veiculoes", "PrecoDia");
        }
    }
}
