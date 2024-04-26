﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEntegrasyon.Migrations
{
    /// <inheritdoc />
    public partial class v06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ikasId",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ikasId",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ikasId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ikasId",
                table: "Brands");
        }
    }
}
