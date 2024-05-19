using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueStarMVC.Migrations
{
    /// <inheritdoc />
    public partial class RestoredMigration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CHUYENBAY_SANBAY_CHUYENBAY_ChuyenbayFlyId",
                table: "CHUYENBAY_SANBAY");

            migrationBuilder.DropForeignKey(
                name: "FK_TICKET_Seats_SeatID",
                table: "TICKET");

            migrationBuilder.DropIndex(
                name: "IX_TICKET_SeatID",
                table: "TICKET");

            migrationBuilder.DropIndex(
                name: "IX_CHUYENBAY_SANBAY_ChuyenbayFlyId",
                table: "CHUYENBAY_SANBAY");

            migrationBuilder.DropColumn(
                name: "SeatID",
                table: "TICKET");

            migrationBuilder.DropColumn(
                name: "ChuyenbayFlyId",
                table: "CHUYENBAY_SANBAY");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TICKET",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TICKET",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "SeatID",
                table: "TICKET",
                type: "varchar(3)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChuyenbayFlyId",
                table: "CHUYENBAY_SANBAY",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TICKET_SeatID",
                table: "TICKET",
                column: "SeatID");

            migrationBuilder.CreateIndex(
                name: "IX_CHUYENBAY_SANBAY_ChuyenbayFlyId",
                table: "CHUYENBAY_SANBAY",
                column: "ChuyenbayFlyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CHUYENBAY_SANBAY_CHUYENBAY_ChuyenbayFlyId",
                table: "CHUYENBAY_SANBAY",
                column: "ChuyenbayFlyId",
                principalTable: "CHUYENBAY",
                principalColumn: "flyID");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKET_Seats_SeatID",
                table: "TICKET",
                column: "SeatID",
                principalTable: "Seats",
                principalColumn: "seatId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
