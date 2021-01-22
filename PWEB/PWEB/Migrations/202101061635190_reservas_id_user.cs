namespace PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservas_id_user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservas", "Id_user", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservas", "Id_user");
            AddForeignKey("dbo.Reservas", "Id_user", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "Id_user", "dbo.AspNetUsers");
            DropIndex("dbo.Reservas", new[] { "Id_user" });
            DropColumn("dbo.Reservas", "Id_user");
        }
    }
}
