namespace Expense.Data.DbContexts.IdentityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Identity.Currency",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Identity.Language",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Identity.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "Identity.UserRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("Identity.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("Identity.UsersInfo", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "Identity.UsersInfo",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BirthDate = c.DateTime(),
                        Gender = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        Picture = c.Binary(),
                        CurrencyId = c.Guid(nullable: false),
                        LanguageId = c.Guid(nullable: false),
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
                .ForeignKey("Identity.Currency", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("Identity.Language", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.CurrencyId)
                .Index(t => t.LanguageId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "Identity.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Identity.UsersInfo", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Identity.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("Identity.UsersInfo", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Identity.UserRoles", "UserId", "Identity.UsersInfo");
            DropForeignKey("Identity.UserLogins", "UserId", "Identity.UsersInfo");
            DropForeignKey("Identity.UsersInfo", "LanguageId", "Identity.Language");
            DropForeignKey("Identity.UsersInfo", "CurrencyId", "Identity.Currency");
            DropForeignKey("Identity.UserClaims", "UserId", "Identity.UsersInfo");
            DropForeignKey("Identity.UserRoles", "RoleId", "Identity.Roles");
            DropIndex("Identity.UserLogins", new[] { "UserId" });
            DropIndex("Identity.UserClaims", new[] { "UserId" });
            DropIndex("Identity.UsersInfo", "UserNameIndex");
            DropIndex("Identity.UsersInfo", new[] { "LanguageId" });
            DropIndex("Identity.UsersInfo", new[] { "CurrencyId" });
            DropIndex("Identity.UserRoles", new[] { "RoleId" });
            DropIndex("Identity.UserRoles", new[] { "UserId" });
            DropIndex("Identity.Roles", "RoleNameIndex");
            DropTable("Identity.UserLogins");
            DropTable("Identity.UserClaims");
            DropTable("Identity.UsersInfo");
            DropTable("Identity.UserRoles");
            DropTable("Identity.Roles");
            DropTable("Identity.Language");
            DropTable("Identity.Currency");
        }
    }
}
