using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEntegrasyon.Migrations
{
    /// <inheritdoc />
    public partial class v04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat01Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat01Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat02Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat02Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat03Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat03Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat04Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat04Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat05Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat05Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat06Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat06Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat07Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cat07Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandDesc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariant",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemDimTypeCode = table.Column<int>(type: "int", nullable: false),
                    ItemDim1Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemDim1Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemDim2Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemDim2Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemDim3Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemDim3Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Vat = table.Column<int>(type: "int", nullable: false),
                    Price1 = table.Column<double>(type: "float", nullable: false),
                    Price2 = table.Column<double>(type: "float", nullable: false),
                    Price3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price4 = table.Column<double>(type: "float", nullable: false),
                    Price5 = table.Column<double>(type: "float", nullable: false),
                    AlisFiyati = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductAtt10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductAtt10Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PAZARYERIISK = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    N11_LST = table.Column<double>(type: "float", nullable: false),
                    N11_IND = table.Column<double>(type: "float", nullable: false),
                    AMAZON_LST = table.Column<double>(type: "float", nullable: false),
                    AMAZON_IND = table.Column<double>(type: "float", nullable: false),
                    CICEK_LST = table.Column<double>(type: "float", nullable: false),
                    CICEK_IND = table.Column<double>(type: "float", nullable: false),
                    GITTIGIDIYOR_LST = table.Column<double>(type: "float", nullable: false),
                    GITTIGIDIYOR_IND = table.Column<double>(type: "float", nullable: false),
                    HEPSIBURADA_LST = table.Column<double>(type: "float", nullable: false),
                    HEPSIBURADA_IND = table.Column<double>(type: "float", nullable: false),
                    MORHIPO_LST = table.Column<double>(type: "float", nullable: false),
                    MORHIPO_IND = table.Column<double>(type: "float", nullable: false),
                    PAZARAMA_LST = table.Column<double>(type: "float", nullable: false),
                    PAZARAMA_IND = table.Column<double>(type: "float", nullable: false),
                    TRENDYOL_LST = table.Column<double>(type: "float", nullable: false),
                    TRENDYOL_IND = table.Column<double>(type: "float", nullable: false),
                    BISIFIRAT_LST = table.Column<double>(type: "float", nullable: false),
                    BISIFIRAT_IND = table.Column<double>(type: "float", nullable: false),
                    TTTURK_LST = table.Column<double>(type: "float", nullable: false),
                    TTTURK_IND = table.Column<double>(type: "float", nullable: false),
                    BREND_LST = table.Column<double>(type: "float", nullable: false),
                    BREND_IND = table.Column<double>(type: "float", nullable: false),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image8 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariant_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_ProductID",
                table: "ProductVariant",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductVariant");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
