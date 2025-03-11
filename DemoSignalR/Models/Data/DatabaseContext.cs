using System;
using System.Collections.Generic;
using DemoSignalR.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoSignalR.Models.Data;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRoom> UserRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__ChatRoom__32863939DD5C28A2");

            entity.HasIndex(e => e.RoomName, "UQ__ChatRoom__6B500B556C213ABB").IsUnique();

            entity.Property(e => e.RoomId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RoomName).HasMaxLength(100);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C0C9CAE8DF6B3");

            entity.Property(e => e.MessageId).HasDefaultValueSql("(newid())");

            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(1000); // Adjust as needed

            entity.Property(e => e.SenderId)
                .IsRequired();

            entity.HasOne(d => d.Room)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_Messages_ChatRooms");

            entity.HasOne(d => d.Sender) // Changed from User to Sender
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK_Messages_Users");
        });


        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C538322D8");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E43977597B").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534E648302A").IsUnique();

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserRoom>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoomId }).HasName("PK__UserRoom__94A0AFDF8E0CB511");

            entity.Property(e => e.JoinedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Room).WithMany(p => p.UserRooms)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_UserRooms_ChatRooms");

            entity.HasOne(d => d.User).WithMany(p => p.UserRooms)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRooms_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
