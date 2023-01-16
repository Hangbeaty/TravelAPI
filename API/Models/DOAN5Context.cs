using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace API.Models
{
    public partial class DOAN5Context : DbContext
    {
        public DOAN5Context()
        {
        }

        public DOAN5Context(DbContextOptions<DOAN5Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<New> News { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-H7QDFO7;Database=DOAN5;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PassWord)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Content).HasMaxLength(500);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerAdress).HasMaxLength(50);

                entity.Property(e => e.CustomerEmail).HasMaxLength(50);

                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.CustomerPhone).HasMaxLength(50);
              entity.Property(e => e.UserName).HasMaxLength(50);
              entity.Property(e => e.Password).HasMaxLength(50);
            });

            modelBuilder.Entity<New>(entity =>
            {
                entity.ToTable("New");

                entity.Property(e => e.NewId).HasColumnName("NewID");

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.NewName).HasMaxLength(500);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.OrderAdress).HasMaxLength(500);
              entity.Property(e => e.Status).HasMaxLength(50);

              entity.Property(e => e.OrderEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderName).HasMaxLength(50);

              entity.Property(e => e.OrderPhone);
                entity.Property(e => e.TourId).HasColumnName("TourID");

                entity.Property(e => e.TourName).HasMaxLength(50);
              entity.Property(e => e.TourName).HasMaxLength(50);
              entity.Property(e => e.TourName).HasMaxLength(50);
              entity.Property(e => e.TourName).HasMaxLength(50);
              entity.Property(e => e.TourName).HasMaxLength(50);
              entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Order_Tour");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("Place");

                entity.Property(e => e.PlaceId).HasColumnName("PlaceID");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.ToTable("Tour");

                entity.Property(e => e.TourId).HasColumnName("TourID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.PlaceId).HasColumnName("PlaceID");
                 entity.Property(e => e.CategoryName).HasMaxLength(50);

              entity.Property(e => e.PlaceName).HasMaxLength(500);

                entity.Property(e => e.Time).HasMaxLength(50);

                entity.Property(e => e.TourName).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Tour_Category");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Tour_Place");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
