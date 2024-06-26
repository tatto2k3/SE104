﻿// <auto-generated />
using System;
using BlueStarMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlueStarMVC.Migrations
{
    [DbContext(typeof(BluestarContext))]
    [Migration("20240429150026_RestoredMigration4")]
    partial class RestoredMigration4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Latin1_General_100_CI_AS_SC_UTF8")
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlueStarMVC.Models.Account", b =>
                {
                    b.Property<string>("Email")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("NAME");

                    b.Property<string>("Password")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("PASSWORD");

                    b.HasKey("Email")
                        .HasName("PK__ACCOUNT__161CF725E0FECA79");

                    b.ToTable("ACCOUNT", (string)null);
                });

            modelBuilder.Entity("BlueStarMVC.Models.Chuyenbay", b =>
                {
                    b.Property<string>("FlyId")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("flyID");

                    b.Property<string>("DepartureDay")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("departureDay");

                    b.Property<string>("DepartureTime")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)")
                        .HasColumnName("departureTime");

                    b.Property<string>("FlightTime")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)")
                        .HasColumnName("flightTime");

                    b.Property<string>("FromLocation")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("fromLocation");

                    b.Property<int?>("NumberOfSeat1")
                        .HasColumnType("int")
                        .HasColumnName("numberOfSeat1");

                    b.Property<int?>("NumberOfSeat2")
                        .HasColumnType("int")
                        .HasColumnName("numberOfSeat2");

                    b.Property<int?>("OriginalPrice")
                        .HasColumnType("int")
                        .HasColumnName("originalPrice");

                    b.Property<string>("ToLocation")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("toLocation");

                    b.HasKey("FlyId")
                        .HasName("PK__CHUYENBA__337DF9F42EAD0A88");

                    b.ToTable("CHUYENBAY", (string)null);
                });

            modelBuilder.Entity("BlueStarMVC.Models.Chuyenbay_Sanbay", b =>
                {
                    b.Property<string>("FlyId")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("flyID");

                    b.Property<string>("AirportId")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("airportID");

                    b.Property<string>("ChuyenbayFlyId")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Note")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("note");

                    b.Property<double?>("Time")
                        .HasColumnType("float")
                        .HasColumnName("time");

                    b.HasKey("FlyId", "AirportId");

                    b.HasIndex("AirportId");

                    b.HasIndex("ChuyenbayFlyId");

                    b.ToTable("CHUYENBAY_SANBAY", (string)null);
                });

            modelBuilder.Entity("BlueStarMVC.Models.Customer", b =>
                {
                    b.Property<string>("CId")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("C_ID");

                    b.Property<string>("Fullname")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("FULLNAME");

                    b.Property<string>("Mail")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("MAIL");

                    b.Property<string>("NumId")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("NUM_ID");

                    b.Property<string>("Point")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("POINT");

                    b.HasKey("CId")
                        .HasName("PK__CUSTOMER__A9FDEC12AFB11635");

                    b.ToTable("CUSTOMER", (string)null);
                });

            modelBuilder.Entity("BlueStarMVC.Models.Discount", b =>
                {
                    b.Property<string>("DId")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("D_ID");

                    b.Property<string>("DFinish")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("D_FINISH");

                    b.Property<string>("DName")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("D_NAME");

                    b.Property<int?>("DPercent")
                        .HasColumnType("int")
                        .HasColumnName("D_PERCENT");

                    b.Property<string>("DStart")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("D_START");

                    b.HasKey("DId")
                        .HasName("pk_discount");

                    b.ToTable("DISCOUNT", (string)null);
                });

            modelBuilder.Entity("BlueStarMVC.Models.Food", b =>
                {
                    b.Property<string>("FId")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("F_ID");

                    b.Property<string>("FName")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("F_NAME");

                    b.Property<string>("FPrice")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("F_PRICE");

                    b.HasKey("FId")
                        .HasName("pk_food");

                    b.ToTable("FOOD", (string)null);
                });

            modelBuilder.Entity("BlueStarMVC.Models.Luggage", b =>
                {
                    b.Property<string>("LuggageCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("LUGGAGE_CODE");

                    b.Property<string>("Mass")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)")
                        .HasColumnName("MASS");

                    b.Property<int?>("Price")
                        .HasColumnType("int")
                        .HasColumnName("PRICE");

                    b.HasKey("LuggageCode")
                        .HasName("PK__LUGGAGE__1C0F361D9705AAFB");

                    b.ToTable("LUGGAGE", (string)null);
                });

            modelBuilder.Entity("BlueStarMVC.Models.Plane", b =>
                {
                    b.Property<string>("PlId")
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnType("varchar(4)")
                        .HasColumnName("PL_ID");

                    b.Property<int?>("BusinessCapacity")
                        .HasColumnType("int")
                        .HasColumnName("BUSINESS_CAPACITY");

                    b.Property<int?>("EconomyCapacity")
                        .HasColumnType("int")
                        .HasColumnName("ECONOMY_CAPACITY");

                    b.Property<string>("Typeofplane")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("TYPEOFPLANE");

                    b.HasKey("PlId")
                        .HasName("pk_plane");

                    b.ToTable("PLANE", (string)null);
                });

            modelBuilder.Entity("BlueStarMVC.Models.Sanbay", b =>
                {
                    b.Property<string>("AirportId")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("airportID");

                    b.Property<string>("AirportName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("airportName");

                    b.Property<string>("Place")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("place");

                    b.HasKey("AirportId")
                        .HasName("PK__SANBAY__C85BDF9E80C3A36E");

                    b.ToTable("SANBAY", (string)null);
                });

            modelBuilder.Entity("BlueStarMVC.Models.Seat", b =>
                {
                    b.Property<string>("SeatID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("percent")
                        .HasColumnType("real");

                    b.HasKey("SeatID");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("BlueStarMVC.Models.Ticket", b =>
                {
                    b.Property<string>("TId")
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnType("varchar(4)")
                        .HasColumnName("T_ID");

                    b.Property<string>("Cccd")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)")
                        .HasColumnName("CCCD");

                    b.Property<string>("FlyId")
                        .IsRequired()
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnType("varchar(4)")
                        .HasColumnName("Fly_ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("SDT");

                    b.Property<string>("SeatID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Seat_Type_ID")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("Seat_ID");

                    b.Property<long>("TicketPrice")
                        .HasColumnType("bigint")
                        .HasColumnName("Ticket_Price");

                    b.HasKey("TId")
                        .HasName("PK__TICKET__83BB1FB2BDECFB45");

                    b.HasIndex("SeatID");

                    b.ToTable("TICKET", (string)null);
                });

            modelBuilder.Entity("BlueStarMVC.Models.Chuyenbay_Sanbay", b =>
                {
                    b.HasOne("BlueStarMVC.Models.Sanbay", "Sanbay")
                        .WithMany("Chuyenbay_Sanbays")
                        .HasForeignKey("AirportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlueStarMVC.Models.Chuyenbay", null)
                        .WithMany("TrungGian")
                        .HasForeignKey("ChuyenbayFlyId");

                    b.HasOne("BlueStarMVC.Models.Chuyenbay", "Chuyenbay")
                        .WithMany("Chuyenbay_Sanbays")
                        .HasForeignKey("FlyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chuyenbay");

                    b.Navigation("Sanbay");
                });

            modelBuilder.Entity("BlueStarMVC.Models.Ticket", b =>
                {
                    b.HasOne("BlueStarMVC.Models.Seat", "Seat")
                        .WithMany()
                        .HasForeignKey("SeatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seat");
                });

            modelBuilder.Entity("BlueStarMVC.Models.Chuyenbay", b =>
                {
                    b.Navigation("Chuyenbay_Sanbays");

                    b.Navigation("TrungGian");
                });

            modelBuilder.Entity("BlueStarMVC.Models.Sanbay", b =>
                {
                    b.Navigation("Chuyenbay_Sanbays");
                });
#pragma warning restore 612, 618
        }
    }
}
