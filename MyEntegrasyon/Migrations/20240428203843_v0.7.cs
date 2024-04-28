using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEntegrasyon.Migrations
{
    /// <inheritdoc />
    public partial class v07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Variant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IkasId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    selectionType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VariantValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantID = table.Column<int>(type: "int", nullable: false),
                    IkasId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantValue_Variant_VariantID",
                        column: x => x.VariantID,
                        principalTable: "Variant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VariantValue_VariantID",
                table: "VariantValue",
                column: "VariantID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VariantValue");

            migrationBuilder.DropTable(
                name: "Variant");
        }
    }
}
