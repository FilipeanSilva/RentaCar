namespace PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entrega : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Entregas", "Data_Entrega_i", c => c.DateTime(nullable: false));
            AddColumn("dbo.Entregas", "Data_Entrega_f", c => c.DateTime(nullable: false));
            DropColumn("dbo.Entregas", "Data_Entrega");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Entregas", "Data_Entrega", c => c.DateTime(nullable: false));
            DropColumn("dbo.Entregas", "Data_Entrega_f");
            DropColumn("dbo.Entregas", "Data_Entrega_i");
        }
    }
}
