namespace PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModeloInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id_categoria = c.Int(nullable: false, identity: true),
                        NomeCategoria = c.String(nullable: false, maxLength: 16),
                        Descricao = c.String(maxLength: 120),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id_categoria);
            
            CreateTable(
                "dbo.Contas",
                c => new
                    {
                        ContaId = c.Int(nullable: false, identity: true),
                        Nome_Conta = c.String(nullable: false, maxLength: 100),
                        Numero_Conta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContaId);
            
            CreateTable(
                "dbo.Entregas",
                c => new
                    {
                        Id_reserva = c.Int(nullable: false, identity: true),
                        Data_Entrega = c.DateTime(nullable: false),
                        Id_veiculo = c.Int(nullable: false),
                        Reserva_Id_reserva = c.Int(),
                    })
                .PrimaryKey(t => t.Id_reserva)
                .ForeignKey("dbo.Reservas", t => t.Reserva_Id_reserva)
                .ForeignKey("dbo.Veiculoes", t => t.Id_veiculo, cascadeDelete: true)
                .Index(t => t.Id_veiculo)
                .Index(t => t.Reserva_Id_reserva);
            
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        Id_reserva = c.Int(nullable: false, identity: true),
                        Datar_i = c.DateTime(nullable: false),
                        Datar_f = c.DateTime(nullable: false),
                        PrecoTotal = c.Double(nullable: false),
                        Disponivel = c.Boolean(nullable: false),
                        Id_veiculo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_reserva)
                .ForeignKey("dbo.Veiculoes", t => t.Id_veiculo, cascadeDelete: true)
                .Index(t => t.Id_veiculo);
            
            CreateTable(
                "dbo.Veiculoes",
                c => new
                    {
                        Id_veiculo = c.Int(nullable: false, identity: true),
                        Nome_veiculo = c.String(nullable: false, maxLength: 16),
                        Marca = c.String(nullable: false, maxLength: 80),
                        Cor = c.String(nullable: false, maxLength: 80),
                        Kilometros = c.Int(nullable: false),
                        Defeitos = c.Int(nullable: false),
                        Id_categoria = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_veiculo)
                .ForeignKey("dbo.Categorias", t => t.Id_categoria, cascadeDelete: true)
                .Index(t => t.Id_categoria);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Id_empresa = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresas", t => t.Id_empresa)
                .Index(t => t.Id_empresa)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        Id_empresa = c.Int(nullable: false, identity: true),
                        Nome_Empresa = c.String(),
                    })
                .PrimaryKey(t => t.Id_empresa);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Utilizadors",
                c => new
                    {
                        Utilizador_Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        ContaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Utilizador_Id)
                .ForeignKey("dbo.Contas", t => t.ContaId, cascadeDelete: true)
                .Index(t => t.ContaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Utilizadors", "ContaId", "dbo.Contas");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Id_empresa", "dbo.Empresas");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Entregas", "Id_veiculo", "dbo.Veiculoes");
            DropForeignKey("dbo.Entregas", "Reserva_Id_reserva", "dbo.Reservas");
            DropForeignKey("dbo.Reservas", "Id_veiculo", "dbo.Veiculoes");
            DropForeignKey("dbo.Veiculoes", "Id_categoria", "dbo.Categorias");
            DropIndex("dbo.Utilizadors", new[] { "ContaId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "Id_empresa" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Veiculoes", new[] { "Id_categoria" });
            DropIndex("dbo.Reservas", new[] { "Id_veiculo" });
            DropIndex("dbo.Entregas", new[] { "Reserva_Id_reserva" });
            DropIndex("dbo.Entregas", new[] { "Id_veiculo" });
            DropTable("dbo.Utilizadors");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Empresas");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Veiculoes");
            DropTable("dbo.Reservas");
            DropTable("dbo.Entregas");
            DropTable("dbo.Contas");
            DropTable("dbo.Categorias");
        }
    }
}
