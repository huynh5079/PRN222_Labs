using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopeeFood4.Models;

namespace ShopeeFood4.Data;

public partial class ShopeeFoodContext : DbContext
{
    public ShopeeFoodContext()
    {
    }

    public ShopeeFoodContext(DbContextOptions<ShopeeFoodContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<DeliveryDriver> DeliveryDrivers { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Administ__719FE4E8AAA0A30D");

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<DeliveryDriver>(entity =>
        {
            entity.HasKey(e => e.DeliveryDriverId).HasName("PK__Delivery__B78ABB168DD79F82");

            entity.Property(e => e.DeliveryDriverId).HasColumnName("DeliveryDriverID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Logs__5E5499A8F6AAC46F");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LogType).HasMaxLength(20);
            entity.Property(e => e.Message).HasMaxLength(2000);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.MenuItemId).HasName("PK__MenuItem__8943F7028E436170");

            entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");
            entity.Property(e => e.AvailabilityStatus).HasMaxLength(20);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK__MenuItems__Resta__2C3393D0");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32C285AB81");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeliveryDriverId).HasColumnName("DeliveryDriverID");
            entity.Property(e => e.Message).HasMaxLength(1000);
            entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.DeliveryDriver).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.DeliveryDriverId)
                .HasConstraintName("FK__Notificat__Deliv__4CA06362");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK__Notificat__Resta__4BAC3F29");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__4AB81AF0");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF403BD05B");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.DeliveryAddress).HasMaxLength(200);
            entity.Property(e => e.DeliveryDriverId).HasColumnName("DeliveryDriverID");
            entity.Property(e => e.OrderStatus).HasMaxLength(20);
            entity.Property(e => e.PlacedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.DeliveryDriver).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeliveryDriverId)
                .HasConstraintName("FK__Orders__Delivery__5812160E");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Orders)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK__Orders__Restaura__571DF1D5");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__UserID__5629CD9C");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED06A14733F532");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MenuItem).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.MenuItemId)
                .HasConstraintName("FK__OrderItem__MenuI__5DCAEF64");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderItem__Order__5CD6CB2B");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.RestaurantId).HasName("PK__Restaura__87454CB5B9D944FB");

            entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.OpenStatus).HasMaxLength(10);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AE7281CEA4");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.Comment).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeliveryDriverId).HasColumnName("DeliveryDriverID");
            entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.DeliveryDriver).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.DeliveryDriverId)
                .HasConstraintName("FK__Reviews__Deliver__3B75D760");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK__Reviews__Restaur__3A81B327");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__UserID__398D8EEE");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A4B58F974E2");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.TransactionStatus).HasMaxLength(20);

            entity.HasOne(d => d.Order).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Transacti__Order__628FA481");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACAE1CE40C");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
