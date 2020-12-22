using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyShop.API.Data.Migrations
{
    public partial class ChangingStructureOfTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Products_ProductDTOProductId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ProductDTOProductId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ProductDTOProductId",
                table: "Tags");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductDTOProductId",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ProductDTOProductId",
                table: "Tags",
                column: "ProductDTOProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Products_ProductDTOProductId",
                table: "Tags",
                column: "ProductDTOProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
