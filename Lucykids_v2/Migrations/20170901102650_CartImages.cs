using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lucykids_v2.Migrations
{
    public partial class CartImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartLineId",
                table: "ProductImages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_CartLineId",
                table: "ProductImages",
                column: "CartLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_CartLines_CartLineId",
                table: "ProductImages",
                column: "CartLineId",
                principalTable: "CartLines",
                principalColumn: "CartLineId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_CartLines_CartLineId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_CartLineId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "CartLineId",
                table: "ProductImages");
        }
    }
}
