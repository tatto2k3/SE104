using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlueStarMVC.Models;

public partial class BluestarContext : DbContext
{
    public BluestarContext()
    {
    }

    public BluestarContext(DbContextOptions<BluestarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Chuyenbay> Chuyenbays { get; set; }


    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Plane> Planes { get; set; }

    public virtual DbSet<Sanbay> Sanbays { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public DbSet<Seat> Seats { get; set; }
    public DbSet<Chuyenbay_Sanbay> Chuyenbay_Sanbays { get; set; }
    public DbSet<Parameter> Parameters { get; set; }

    public DbSet<Storage> Storages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=BLUESTAR;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_100_CI_AS_SC_UTF8");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__ACCOUNT__161CF725E0FECA79");

            entity.ToTable("ACCOUNT");

            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("PASSWORD");
        });

        modelBuilder.Entity<Chuyenbay>(entity =>
        {
            entity.HasKey(e => e.FlyId).HasName("PK__CHUYENBA__337DF9F42EAD0A88");

            entity.ToTable("CHUYENBAY");

            entity.Property(e => e.FlyId)
                .HasMaxLength(20)
                .HasColumnName("flyID");
            entity.Property(e => e.OriginalPrice)
                .HasColumnType("int")
                .HasColumnName("originalPrice");
            entity.Property(e => e.FromLocation)
                .HasMaxLength(255)
                .HasColumnName("fromLocation");
            entity.Property(e => e.ToLocation)
                .HasMaxLength(255)
                .HasColumnName("toLocation");
            entity.Property(e => e.DepartureDay)
                .HasMaxLength(10)
                .HasColumnName("departureDay");
            entity.Property(e => e.DepartureTime)
                .HasMaxLength(5)
                .HasColumnName("departureTime");
            entity.Property(e => e.FlightTime)
                .HasMaxLength(5)
                .HasColumnName("flightTime");
            entity.Property(e => e.SeatEmpty)
                .HasColumnType("int")
                .HasColumnName("SeatEmpty");
            entity.Property(e => e.SeatBooked)
                .HasColumnType("int")
                .HasColumnName("SeatBooked");

        });

        

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DId).HasName("pk_discount");

            entity.ToTable("DISCOUNT");

            entity.Property(e => e.DId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("D_ID");
            entity.Property(e => e.DFinish)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("D_FINISH");
            entity.Property(e => e.DName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("D_NAME");
            entity.Property(e => e.DPercent).HasColumnName("D_PERCENT");
            entity.Property(e => e.DStart)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("D_START");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.SeatID).HasName("pk_seat");

            entity.ToTable("Seats");

            entity.Property(e => e.SeatID)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("seatId");
            entity.Property(e => e.percent)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("percent");
        });


        modelBuilder.Entity<Plane>(entity =>
        {
            entity.HasKey(e => e.PlId).HasName("pk_plane");

            entity.ToTable("PLANE");

            entity.Property(e => e.PlId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("PL_ID");
            entity.Property(e => e.BusinessCapacity).HasColumnName("BUSINESS_CAPACITY");
            entity.Property(e => e.EconomyCapacity).HasColumnName("ECONOMY_CAPACITY");
            entity.Property(e => e.Typeofplane)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TYPEOFPLANE");
        });

        modelBuilder.Entity<Sanbay>(entity =>
        {
            entity.HasKey(e => e.AirportId).HasName("PK__SANBAY__C85BDF9E80C3A36E");

            entity.ToTable("SANBAY");

            entity.Property(e => e.AirportId)
                .HasMaxLength(20)
                .HasColumnName("airportID");
            entity.Property(e => e.AirportName)
                .HasMaxLength(255)
                .HasColumnName("airportName");
            entity.Property(e => e.Place)
                .HasMaxLength(255)
                .HasColumnName("place");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TId).HasName("PK__TICKET__83BB1FB2BDECFB45");

            entity.ToTable("TICKET");


            entity.Property(e => e.TId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("T_ID");
            entity.Property(e => e.Cccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("CCCD");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name");
            entity.Property(e => e.FlyId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Fly_ID");
            entity.Property(e => e.Seat_Type_ID)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Seat_ID");
            entity.Property(e => e.TicketPrice)
            .HasColumnName("Ticket_Price")
            .HasColumnType("bigint");
            entity.Property(e => e.SDT)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SDT");
        });



        modelBuilder.Entity<Chuyenbay_Sanbay>(entity =>
        {
            entity.HasKey(e => new { e.FlyId, e.AirportId });

            entity.ToTable("CHUYENBAY_SANBAY");

            entity.Property(e => e.FlyId)
                .HasMaxLength(20)
                .HasColumnName("flyID");

            entity.Property(e => e.AirportId)
                .HasMaxLength(20)
                .HasColumnName("airportID");

            entity.Property(e => e.Time)
                .HasColumnName("time")
                .HasColumnType("float");

            entity.Property(e => e.Note)
                .HasMaxLength(100)
                .HasColumnName("note");


        });

        modelBuilder.Entity<Parameter>(entity =>
        {
            entity.HasKey(e => e.Label).HasName("PK__LABEL__C85BDF9E80C3A36E");

            entity.ToTable("PARAMETER");

            entity.Property(e => e.Label)
                .HasMaxLength(100)
                .HasColumnName("label");
            entity.Property(e => e.Value)
                .HasColumnType("int")
                .HasColumnName("value");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
