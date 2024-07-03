using Microsoft.EntityFrameworkCore;

namespace CRS.WebApi.Models;

public partial class CrsdbContext : DbContext
{
    public CrsdbContext()
    {
    }

    public CrsdbContext(DbContextOptions<CrsdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TaxPayer> TaxPayers { get; set; }

    public virtual DbSet<TaxPayerType> TaxPayerTypes { get; set; }

    public virtual DbSet<TaxPayment> TaxPayments { get; set; }

    public virtual DbSet<TaxStatus> TaxStatuses { get; set; }

    public virtual DbSet<TaxType> TaxTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<TaxRecordViewModel> TaxRecordView { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DBCon");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxPayer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TaxPayerId");

            entity.ToTable("TaxPayer", tb => tb.HasTrigger("trgAfterUpdate"));

            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TaxPayerId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Updated).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.GroupNavigation).WithMany(p => p.TaxPayers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaxPayer_TaxPyerType");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.TaxPayers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaxPayer_TaxStatus");
        });

        modelBuilder.Entity<TaxPayerType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TaxPayerTypeId");

            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TaxPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TaxPaymentId");

            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.TaxPayer).WithMany(p => p.TaxPayments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaxPayer_TaxPayment");

            entity.HasOne(d => d.TaxTypeNavigation).WithMany(p => p.TaxPayments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaxPayment_TaxType");
        });

        modelBuilder.Entity<TaxStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TaxStatusId");
        });

        modelBuilder.Entity<TaxType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TaxTypeId");

            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserId");

            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TaxRecordViewModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
