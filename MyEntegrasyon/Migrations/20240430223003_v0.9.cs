using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEntegrasyon.Migrations
{
    /// <inheritdoc />
    public partial class v09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValue_Variant_VariantID",
                table: "VariantValue");

            migrationBuilder.RenameColumn(
                name: "VariantID",
                table: "VariantValue",
                newName: "VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_VariantValue_VariantID",
                table: "VariantValue",
                newName: "IX_VariantValue_VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValue_Variant_VariantId",
                table: "VariantValue",
                column: "VariantId",
                principalTable: "Variant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValue_Variant_VariantId",
                table: "VariantValue");

            migrationBuilder.RenameColumn(
                name: "VariantId",
                table: "VariantValue",
                newName: "VariantID");

            migrationBuilder.RenameIndex(
                name: "IX_VariantValue_VariantId",
                table: "VariantValue",
                newName: "IX_VariantValue_VariantID");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValue_Variant_VariantID",
                table: "VariantValue",
                column: "VariantID",
                principalTable: "Variant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
