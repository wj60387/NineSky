using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ninesky.Web.Migrations
{
    public partial class updateCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Module",
                table: "CategoryGeneral");

            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "CategoryGeneral",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "CategoryGeneral");

            migrationBuilder.AddColumn<string>(
                name: "Module",
                table: "CategoryGeneral",
                maxLength: 50,
                nullable: true);
        }
    }
}
