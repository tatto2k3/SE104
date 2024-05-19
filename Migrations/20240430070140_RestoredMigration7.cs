using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueStarMVC.Migrations
{
    /// <inheritdoc />
    public partial class RestoredMigration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CHUYENBAY_SANBAY_CHUYENBAY_flyID",
                table: "CHUYENBAY_SANBAY");

            migrationBuilder.DropForeignKey(
                name: "FK_CHUYENBAY_SANBAY_SANBAY_airportID",
                table: "CHUYENBAY_SANBAY");

            migrationBuilder.DropIndex(
                name: "IX_CHUYENBAY_SANBAY_airportID",
                table: "CHUYENBAY_SANBAY");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CHUYENBAY_SANBAY_airportID",
                table: "CHUYENBAY_SANBAY",
                column: "airportID");

            migrationBuilder.AddForeignKey(
                name: "FK_CHUYENBAY_SANBAY_CHUYENBAY_flyID",
                table: "CHUYENBAY_SANBAY",
                column: "flyID",
                principalTable: "CHUYENBAY",
                principalColumn: "flyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CHUYENBAY_SANBAY_SANBAY_airportID",
                table: "CHUYENBAY_SANBAY",
                column: "airportID",
                principalTable: "SANBAY",
                principalColumn: "airportID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
