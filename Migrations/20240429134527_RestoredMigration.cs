using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueStarMVC.Migrations
{
    /// <inheritdoc />
    public partial class RestoredMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACCOUNT",
                columns: table => new
                {
                    EMAIL = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    POSITION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ACCOUNT__161CF725E0FECA79", x => x.EMAIL);
                });

            migrationBuilder.CreateTable(
                name: "CHUYENBAY",
                columns: table => new
                {
                    flyID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    originalPrice = table.Column<int>(type: "int", nullable: true),
                    fromLocation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    toLocation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    departureTime = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    departureDay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FlightTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numberOfSeat1 = table.Column<int>(type: "int", nullable: true),
                    numberOfSeat2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CHUYENBA__337DF9F42EAD0A88", x => x.flyID);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMER",
                columns: table => new
                {
                    C_ID = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    NUM_ID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    FULLNAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    POINT = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    MAIL = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CUSTOMER__A9FDEC12AFB11635", x => x.C_ID);
                });

            migrationBuilder.CreateTable(
                name: "DISCOUNT",
                columns: table => new
                {
                    D_ID = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    D_NAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    D_START = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    D_FINISH = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    D_PERCENT = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discount", x => x.D_ID);
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

            migrationBuilder.CreateTable(
                name: "PLANE",
                columns: table => new
                {
                    PL_ID = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    TYPEOFPLANE = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    BUSINESS_CAPACITY = table.Column<int>(type: "int", nullable: true),
                    ECONOMY_CAPACITY = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_plane", x => x.PL_ID);
                });

            migrationBuilder.CreateTable(
                name: "SANBAY",
                columns: table => new
                {
                    airportID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    airportName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    place = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SANBAY__C85BDF9E80C3A36E", x => x.airportID);
                });

            migrationBuilder.CreateTable(
                name: "SEAT",
                columns: table => new
                {
                    SEAT_ID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    SEAT_TYPE = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FLIGHT_ID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ISBOOKED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SEAT__79B89923D3A743A3", x => x.SEAT_ID);
                });

            migrationBuilder.CreateTable(
                name: "TICKET",
                columns: table => new
                {
                    T_ID = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    CCCD = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Fly_ID = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    Kg_ID = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    Seat_ID = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    Food_ID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    Ticket_Price = table.Column<long>(type: "bigint", nullable: false),
                    Mail = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Dis_ID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TICKET__83BB1FB2BDECFB45", x => x.T_ID);
                });

            migrationBuilder.CreateTable(
                name: "CHUYENBAY_SANBAY",
                columns: table => new
                {
                    flyID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    airportID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    time = table.Column<double>(type: "float", nullable: true),
                    note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ChuyenbayFlyId = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHUYENBAY_SANBAY", x => new { x.flyID, x.airportID });
                    table.ForeignKey(
                        name: "FK_CHUYENBAY_SANBAY_CHUYENBAY_ChuyenbayFlyId",
                        column: x => x.ChuyenbayFlyId,
                        principalTable: "CHUYENBAY",
                        principalColumn: "flyID");
                    table.ForeignKey(
                        name: "FK_CHUYENBAY_SANBAY_CHUYENBAY_flyID",
                        column: x => x.flyID,
                        principalTable: "CHUYENBAY",
                        principalColumn: "flyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHUYENBAY_SANBAY_SANBAY_airportID",
                        column: x => x.airportID,
                        principalTable: "SANBAY",
                        principalColumn: "airportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CHUYENBAY_SANBAY_airportID",
                table: "CHUYENBAY_SANBAY",
                column: "airportID");

            migrationBuilder.CreateIndex(
                name: "IX_CHUYENBAY_SANBAY_ChuyenbayFlyId",
                table: "CHUYENBAY_SANBAY",
                column: "ChuyenbayFlyId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCOUNT");

            migrationBuilder.DropTable(
                name: "CHUYENBAY_SANBAY");

            migrationBuilder.DropTable(
                name: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "DISCOUNT");

            migrationBuilder.DropTable(
                name: "FOOD");

            migrationBuilder.DropTable(
                name: "LUGGAGE");

            migrationBuilder.DropTable(
                name: "PLANE");

            migrationBuilder.DropTable(
                name: "SEAT");

            migrationBuilder.DropTable(
                name: "TICKET");

            migrationBuilder.DropTable(
                name: "CHUYENBAY");

            migrationBuilder.DropTable(
                name: "SANBAY");
        }
    }
}
