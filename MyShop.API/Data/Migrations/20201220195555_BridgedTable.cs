using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyShop.API.Data.Migrations
{
    public partial class BridgedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("7420759e-7091-4ed0-81bd-216e910a47b9"));

            migrationBuilder.CreateTable(
                name: "PTBridges",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PTBridges");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("7420759e-7091-4ed0-81bd-216e910a47b9"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
