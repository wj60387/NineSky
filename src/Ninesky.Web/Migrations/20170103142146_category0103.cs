using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ninesky.Web.Migrations
{
    public partial class category0103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "View",
                table: "CategoryPage");

            migrationBuilder.DropColumn(
                name: "View",
                table: "CategoryGeneral");

            migrationBuilder.AddColumn<string>(
                name: "View",
                table: "Categories",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "View",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "View",
                table: "CategoryPage",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "View",
                table: "CategoryGeneral",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
