using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace api;

public partial class ApiContext : DbContext
{
    public ApiContext()
    {
    }

    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=api.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.Mail, "IX_User_Mail").IsUnique();

            entity.HasIndex(e => e.Number, "IX_User_Number").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("strftime('%s', 'now')")
                .HasColumnType("NUMERIC");
            entity.Property(e => e.Dob)
                .HasColumnType("NUMERIC")
                .HasColumnName("DOB");
            entity.Property(e => e.Gender).HasColumnType("NUMERIC");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("1")
                .HasColumnType("NUMERIC");
            entity.Property(e => e.LastLogin)
                .HasDefaultValueSql("strftime('%s', 'now')")
                .HasColumnType("NUMERIC");
            entity.Property(e => e.Role)
                .HasDefaultValueSql("1")
                .HasColumnType("NUMERIC");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("strftime('%s', 'now')")
                .HasColumnType("NUMERIC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
