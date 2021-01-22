namespace PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Defeitoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_reserva = c.Int(nullable: false),
                        DanosExteriores = c.String(maxLength: 500),
                        DanosInteriores = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservas", t => t.Id_reserva, cascadeDelete: true)
                .Index(t => t.Id_reserva);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Defeitoes", "Id_reserva", "dbo.Reservas");
            DropIndex("dbo.Defeitoes", new[] { "Id_reserva" });
            DropTable("dbo.Defeitoes");
        }
    }
}
