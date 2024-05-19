using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueStarMVC.Migrations
{
    /// <inheritdoc />
    public partial class RestoredMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "POSITION",
                table: "ACCOUNT");

            migrationBuilder.RenameColumn(
                name: "FlightTime",
                table: "CHUYENBAY",
                newName: "flightTime");

            migrationBuilder.AlterColumn<string>(
                name: "flightTime",
                table: "CHUYENBAY",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "flightTime",
                table: "CHUYENBAY",
                newName: "FlightTime");

            migrationBuilder.AlterColumn<string>(
                name: "FlightTime",
                table: "CHUYENBAY",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "POSITION",
                table: "ACCOUNT",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
