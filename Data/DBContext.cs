using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

/// <summary>
/// The primary Entity Framework Core database context for the application.
/// Responsible for managing entity sets, configuring the connection to the SQLite database,
/// and mapping entity properties to database columns with constraints and defaults.
/// </summary>
public partial class DBContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the DBContext class.
    /// </summary>
    public DBContext()
    {
    }

    /// <summary>
    /// Initializes a new instance of the DBContext class.
    /// Sets up the database context with options such as database provider and connection string.
    /// </summary>
    /// <param name="options">DbContextOptions that specify configuration settings for the context.</param>
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Table representing the Users in the database.
    /// Provides access to user entities for querying and CRUD operations.
    /// </summary>
    public virtual DbSet<User> Users { get; set; }

    /// <summary>
    /// Configures the database connection and other options for this context instance.
    /// </summary>
    /// <param name="optionsBuilder">Provides settings for context such as connection string management.</param>
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source=api.db");

    /// <summary>
    /// Configures entity mappings, table names, properties, indexes, and default values.
    /// </summary>
    /// <param name="modelBuilder">Used to describe the shape of entities, relationships, and constraints.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasIndex(e => e.Mail, "IX_User_Mail").IsUnique();
            entity.HasIndex(e => e.Number, "IX_User_Number").IsUnique();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("strftime('%s', 'now')").HasColumnType("NUMERIC");
            entity.Property(e => e.Dob).HasColumnType("NUMERIC").HasColumnName("DOB");
            entity.Property(e => e.Gender).HasColumnType("NUMERIC");
            entity.Property(e => e.IsActive).HasDefaultValueSql("1").HasColumnType("NUMERIC");
            entity.Property(e => e.LastLogin).HasDefaultValueSql("strftime('%s', 'now')").HasColumnType("NUMERIC");
            entity.Property(e => e.Role).HasDefaultValueSql("1").HasColumnType("NUMERIC");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("strftime('%s', 'now')").HasColumnType("NUMERIC");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    /// <summary>
    /// Extension point for further entity model customization from partial classes.
    /// </summary>
    /// <param name="modelBuilder">Allows additional configuration in partial class extensions.</param>
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
