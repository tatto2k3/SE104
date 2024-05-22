using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueStarMVC.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "numberOfSeat2",
                table: "CHUYENBAY",
                newName: "SeatEmpty");

            migrationBuilder.RenameColumn(
                name: "numberOfSeat1",
                table: "CHUYENBAY",
                newName: "SeatBooked");

            migrationBuilder.CreateTable(
                name: "PARAMETER",
                columns: table => new
                {
                    label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    value = table.Column<int>(type: "int", nullable: true),
                    valueBefore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LABEL__C85BDF9E80C3A36E", x => x.label);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PARAMETER");

            migrationBuilder.RenameColumn(
                name: "SeatEmpty",
                table: "CHUYENBAY",
                newName: "numberOfSeat2");

            migrationBuilder.RenameColumn(
                name: "SeatBooked",
                table: "CHUYENBAY",
                newName: "numberOfSeat1");
        }
    }
}
