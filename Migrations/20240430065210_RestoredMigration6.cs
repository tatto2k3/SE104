using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueStarMVC.Migrations
{
    /// <inheritdoc />
    public partial class RestoredMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "FOOD");

            migrationBuilder.DropTable(
                name: "LUGGAGE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.RenameColumn(
                name: "SeatID",
                table: "Seats",
                newName: "seatId");

            migrationBuilder.AlterColumn<string>(
                name: "SeatID",
                table: "TICKET",
                type: "varchar(3)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "seatId",
                table: "Seats",
                type: "varchar(3)",
                unicode: false,
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "pk_seat",
                table: "Seats",
                column: "seatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_seat",
                table: "Seats");

            migrationBuilder.RenameColumn(
                name: "seatId",
                table: "Seats",
                newName: "SeatID");

            migrationBuilder.AlterColumn<string>(
                name: "SeatID",
                table: "TICKET",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(3)");

            migrationBuilder.AlterColumn<string>(
                name: "SeatID",
                table: "Seats",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldUnicode: false,
                oldMaxLength: 3);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                column: "SeatID");

            migrationBuilder.CreateTable(
                name: "CUSTOMER",
                columns: table => new
                {
                    C_ID = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    FULLNAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    MAIL = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NUM_ID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    POINT = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CUSTOMER__A9FDEC12AFB11635", x => x.C_ID);
                });

            migrationBuilder.CreateTable(
                name: "FOOD",
                columns: table => new
                {
                    F_ID = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    F_NAME = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    F_PRICE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_food", x => x.F_ID);
                });

            migrationBuilder.CreateTable(
                name: "LUGGAGE",
                columns: table => new
                {
                    LUGGAGE_CODE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    MASS = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    PRICE = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LUGGAGE__1C0F361D9705AAFB", x => x.LUGGAGE_CODE);
                });
        }
    }
}
