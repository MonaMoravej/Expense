using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCodeCamp.Data.Migrations
{
    public partial class testAfter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tests_test2s_test_Id",
                table: "tests");

            migrationBuilder.DropIndex(
                name: "IX_tests_test_Id",
                table: "tests");

            migrationBuilder.DropColumn(
                name: "test_Id",
                table: "tests");

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "tests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tests_ProgramId",
                table: "tests",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_tests_test2s_ProgramId",
                table: "tests",
                column: "ProgramId",
                principalTable: "test2s",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tests_test2s_ProgramId",
                table: "tests");

            migrationBuilder.DropIndex(
                name: "IX_tests_ProgramId",
                table: "tests");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "tests");

            migrationBuilder.AddColumn<int>(
                name: "test_Id",
                table: "tests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tests_test_Id",
                table: "tests",
                column: "test_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tests_test2s_test_Id",
                table: "tests",
                column: "test_Id",
                principalTable: "test2s",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
