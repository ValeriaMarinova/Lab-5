using AutoRepair.DataAccess.Domain;
using Microsoft.EntityFrameworkCore;

namespace AutoRepair.DataAccess.Context
{
    public partial class AutoRepairContext : DbContext
    {
        public AutoRepairContext()
        {
        }

        public AutoRepairContext(DbContextOptions<AutoRepairContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<RepairItem> Repairitems { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Database=autorepair;Host=localhost;Username=postgres;Password=admin;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("FK_Orders_Customers_idx");

                entity.HasIndex(e => e.VehicleId)
                    .HasName("FK_Orders_Vehicles_idx");

                entity.HasIndex(e => e.WorkerId)
                    .HasName("fk_Orders_Workers_idx");

                entity.Property(e => e.ProblemDescription).IsRequired();

                entity.Property(e => e.Solution).IsRequired();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Vehicles");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Workers");
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.ToTable("parts");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<RepairItem>(entity =>
            {
                entity.ToTable("repairitems");

                entity.HasIndex(e => e.OrderId)
                    .HasName("FK_RepairItems_Orders_idx");

                entity.HasIndex(e => e.PartId)
                    .HasName("FK_RepairItems_Parts_idx");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Repairitems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepairItems_Orders");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.Repairitems)
                    .HasForeignKey(d => d.PartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepairItems_Parts");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("vehicles");

                entity.Property(e => e.Make).IsRequired();

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.RegistrationPlate).HasMaxLength(45);
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.ToTable("workers");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Position).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
