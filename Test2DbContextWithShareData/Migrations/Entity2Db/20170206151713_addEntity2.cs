using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Test2DbContextWithShareData.Migrations.Entity2Db
{
    public partial class addEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.EnsureSchema(
                name: "two");

           

            migrationBuilder.CreateTable(
                name: "Entities2",
                schema: "two",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Entity1Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entities2_Entities1_Entity1Id",
                        column: x => x.Entity1Id,
                        principalSchema: "one",
                        principalTable: "Entities1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entities2_Entity1Id",
                schema: "two",
                table: "Entities2",
                column: "Entity1Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entities2",
                schema: "two");

        }
    }
}
