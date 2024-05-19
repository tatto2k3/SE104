using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueStarMVC.Migrations
{
    /// <inheritdoc />
    public partial class RestoredMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_Fly_Seat",
                table: "TICKET");

            migrationBuilder.DropIndex(
                name: "UQ_Fly_Seat_CCCD_Name_Mail",
                table: "TICKET");

            migrationBuilder.DropPrimaryKey(
                name: "PK__SEAT__79B89923D3A743A3",
                table: "SEAT");

            migrationBuilder.DropColumn(
                name: "Dis_ID",
                table: "TICKET");

            migrationBuilder.DropColumn(
                name: "Food_ID",
                table: "TICKET");

            migrationBuilder.DropColumn(
                name: "Kg_ID",
                table: "TICKET");

            migrationBuilder.DropColumn(
                name: "Mail",
                table: "TICKET");

            migrationBuilder.DropColumn(
                name: "FLIGHT_ID",
                table: "SEAT");

            migrationBuilder.DropColumn(
                name: "ISBOOKED",
                table: "SEAT");

            migrationBuilder.DropColumn(
                name: "SEAT_TYPE",
                table: "SEAT");

            migrationBuilder.RenameTable(
                name: "SEAT",
                newName: "Seats");

            migrationBuilder.RenameColumn(
                name: "SEAT_ID",
                table: "Seats",
                newName: "SeatID");

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
                name: "SDT",
                table: "TICKET",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeatID",
                table: "TICKET",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SeatID",
                table: "Seats",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10);

            migrationBuilder.AddColumn<float>(
                name: "percent",
                table: "Seats",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                column: "SeatID");

            migrationBuilder.CreateIndex(
                name: "IX_TICKET_SeatID",
                table: "TICKET",
                column: "SeatID");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKET_Seats_SeatID",
                table: "TICKET",
                column: "SeatID",
                principalTable: "Seats",
                principalColumn: "SeatID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TICKET_Seats_SeatID",
                table: "TICKET");

            migrationBuilder.DropIndex(
                name: "IX_TICKET_SeatID",
                table: "TICKET");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "SDT",
                table: "TICKET");

            migrationBuilder.DropColumn(
                name: "SeatID",
                table: "TICKET");

            migrationBuilder.DropColumn(
                name: "percent",
                table: "Seats");

            migrationBuilder.RenameTable(
                name: "Seats",
                newName: "SEAT");

            migrationBuilder.RenameColumn(
                name: "SeatID",
                table: "SEAT",
                newName: "SEAT_ID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TICKET",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Dis_ID",
                table: "TICKET",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Food_ID",
                table: "TICKET",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kg_ID",
                table: "TICKET",
                type: "varchar(3)",
                unicode: false,
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "TICKET",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SEAT_ID",
                table: "SEAT",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "FLIGHT_ID",
                table: "SEAT",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ISBOOKED",
                table: "SEAT",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SEAT_TYPE",
                table: "SEAT",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK__SEAT__79B89923D3A743A3",
                table: "SEAT",
                column: "SEAT_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Fly_Seat",
                table: "TICKET",
                columns: new[] { "Fly_ID", "Seat_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Fly_Seat_CCCD_Name_Mail",
                table: "TICKET",
                columns: new[] { "Fly_ID", "Seat_ID", "CCCD", "Name", "Mail" },
                unique: true);
        }
    }
}
