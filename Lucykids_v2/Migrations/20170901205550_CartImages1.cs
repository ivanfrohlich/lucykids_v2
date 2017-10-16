using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lucykids_v2.Migrations
{
    public partial class CartImages1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_PieProductId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "PieProductId",
                table: "OrderDetails",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_PieProductId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderDetails",
                newName: "PieProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_PieProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_PieProductId",
                table: "OrderDetails",
                column: "PieProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
