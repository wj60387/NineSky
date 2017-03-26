using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ninesky.Web.Migrations
{
    public partial class _170326 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryGeneral");

            migrationBuilder.DropTable(
                name: "CategoryLink");

            migrationBuilder.DropTable(
                name: "CategoryPage");

            migrationBuilder.AlterColumn<string>(
                name: "View",
                table: "Categories",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChildNum",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContentModuleId",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContentOrder",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContentView",
                table: "Categories",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Creater",
                table: "Categories",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Depth",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LinkUrl",
                table: "Categories",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Meta_Description",
                table: "Categories",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Meta_Keywords",
                table: "Categories",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageSize",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PicUrl",
                table: "Categories",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowOnMenu",
                table: "Categories",
                maxLength: 255,
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildNum",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ContentModuleId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ContentOrder",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ContentView",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Creater",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Depth",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LinkUrl",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Meta_Description",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Meta_Keywords",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PageSize",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PicUrl",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ShowOnMenu",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "View",
                table: "Categories",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryGeneral",
                columns: table => new
                {
                    GeneralId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    ContentOrder = table.Column<int>(nullable: true),
                    ContentView = table.Column<string>(maxLength: 200, nullable: true),
                    ModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGeneral", x => x.GeneralId);
                    table.ForeignKey(
                        name: "FK_CategoryGeneral_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryLink",
                columns: table => new
                {
                    LinkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLink", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_CategoryLink_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryPage",
                columns: table => new
                {
                    PageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(maxLength: 10000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPage", x => x.PageId);
                    table.ForeignKey(
                        name: "FK_CategoryPage_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGeneral_CategoryId",
                table: "CategoryGeneral",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLink_CategoryId",
                table: "CategoryLink",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPage_CategoryId",
                table: "CategoryPage",
                column: "CategoryId",
                unique: true);
        }
    }
}
