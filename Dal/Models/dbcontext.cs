using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal.Models;

public partial class dbcontext : DbContext
{
    public dbcontext()
    {
    }

    public dbcontext(DbContextOptions<dbcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassToFlight> ClassToFlights { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Destination> Destinations { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrdersDetail> OrdersDetails { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<ThisFlight> ThisFlights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\תיקייה כללית חדש\\שנה ב תשפה\\קבוצה א\\תלמידות\\000000פרויקט מלי וכבי\\פרויקט\\projctTours\\database\\database.mdf;Integrated Security=True;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0776ECB9E6");

            entity.ToTable("Class");

            entity.HasIndex(e => e.Description, "IX_Class_Column").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("description");
        });

        modelBuilder.Entity<ClassToFlight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ClassToF__3213E83FE34FC5C5");

            entity.ToTable("ClassToFlight");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassId).HasColumnName("classId");
            entity.Property(e => e.NumberOfSeats).HasColumnName("numberOfSeats ");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Sold).HasColumnName("sold");
            entity.Property(e => e.ThisflightId).HasColumnName("thisflightId");
            entity.Property(e => e.WeightLoad).HasColumnName("weightLoad");

            entity.HasOne(d => d.Class).WithMany(p => p.ClassToFlights)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClassToFl__class__17C286CF");

            entity.HasOne(d => d.Thisflight).WithMany(p => p.ClassToFlights)
                .HasForeignKey(d => d.ThisflightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClassToFl__thisf__11158940");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3213E83FDD205449");

            entity.ToTable("Customer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("firstName");
            entity.Property(e => e.IsManager).HasColumnName("isManager");
            entity.Property(e => e.LastName)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Destination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07C3E93BA1");

            entity.ToTable("Destination");

            entity.HasIndex(e => e.Destination1, "IX_Class_Column").IsUnique();

            entity.Property(e => e.Destination1)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("destination");
            entity.Property(e => e.Path)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("path");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07449C5F87");

            entity.ToTable("Flight");

            entity.Property(e => e.Sold).HasColumnName("sold");
            entity.Property(e => e.TimeOfFlight).HasColumnName("timeOfFlight");

            entity.HasOne(d => d.DestinationNavigation).WithMany(p => p.FlightDestinationNavigations)
                .HasForeignKey(d => d.Destination)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Flight__Destinat__16CE6296");

            entity.HasOne(d => d.SourceNavigation).WithMany(p => p.FlightSourceNavigations)
                .HasForeignKey(d => d.Source)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Flight__Source__15DA3E5D");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC074D115597");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__idCustom__308E3499");
        });

        modelBuilder.Entity<OrdersDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07DF1B8C04");

            entity.Property(e => e.CountOverLoad).HasColumnName("countOverLoad");
            entity.Property(e => e.CountTickets).HasColumnName("countTickets");
            entity.Property(e => e.IdClassToFlight).HasColumnName("idClassToFlight");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.IdClassToFlightNavigation).WithMany(p => p.OrdersDetails)
                .HasForeignKey(d => d.IdClassToFlight)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrdersDet__idCla__3FD07829");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdersDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrdersDet__order__318258D2");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Table__3214EC07B2E49AEF");

            entity.ToTable("Table");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ThisFlight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ThisFlig__3213E83FE0A35B25");

            entity.ToTable("ThisFlight");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.FlightId).HasColumnName("flightId");
            entity.Property(e => e.PriceToOverLoad)
                .HasColumnType("money")
                .HasColumnName("priceToOverLoad");

            entity.HasOne(d => d.Flight).WithMany(p => p.ThisFlights)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ThisFligh__fligh__10216507");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
