namespace PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Entregas");
            DropForeignKey("dbo.Entregas", "Id_veiculo", "dbo.Veiculoes");
            DropForeignKey("dbo.Entregas", "Reserva_Id_reserva", "dbo.Reservas");
            DropIndex("dbo.Entregas", new[] { "Id_veiculo" });
            DropIndex("dbo.Entregas", new[] { "Reserva_Id_reserva" });
            DropColumn("dbo.Entregas", "Id_reserva");
            RenameColumn(table: "dbo.Entregas", name: "Reserva_Id_reserva", newName: "Id_reserva");
            AddColumn("dbo.Entregas", "Id_Entrega", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Entregas", "Id_reserva", c => c.Int(nullable: false));
            AlterColumn("dbo.Entregas", "Id_reserva", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Entregas", "Id_Entrega");
            CreateIndex("dbo.Entregas", "Id_reserva");
            AddForeignKey("dbo.Entregas", "Id_reserva", "dbo.Reservas", "Id_reserva", cascadeDelete: true);
            DropColumn("dbo.Entregas", "Id_veiculo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Entregas", "Id_veiculo", c => c.Int(nullable: false));
            DropForeignKey("dbo.Entregas", "Id_reserva", "dbo.Reservas");
            DropIndex("dbo.Entregas", new[] { "Id_reserva" });
            DropPrimaryKey("dbo.Entregas");
            AlterColumn("dbo.Entregas", "Id_reserva", c => c.Int());
            AlterColumn("dbo.Entregas", "Id_reserva", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Entregas", "Id_Entrega");
            AddPrimaryKey("dbo.Entregas", "Id_reserva");
            RenameColumn(table: "dbo.Entregas", name: "Id_reserva", newName: "Reserva_Id_reserva");
            AddColumn("dbo.Entregas", "Id_reserva", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Entregas", "Reserva_Id_reserva");
            CreateIndex("dbo.Entregas", "Id_veiculo");
            AddForeignKey("dbo.Entregas", "Reserva_Id_reserva", "dbo.Reservas", "Id_reserva");
            AddForeignKey("dbo.Entregas", "Id_veiculo", "dbo.Veiculoes", "Id_veiculo", cascadeDelete: true);
        }
    }
}
