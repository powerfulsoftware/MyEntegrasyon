using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEntegrasyon.Migrations
{
    /// <inheritdoc />
    public partial class v03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Islem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IslemAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JsonDesenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Islem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Islem_jsonDesen_JsonDesenId",
                        column: x => x.JsonDesenId,
                        principalTable: "jsonDesen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Islem_JsonDesenId",
                table: "Islem",
                column: "JsonDesenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Islem");
        }
    }
}
