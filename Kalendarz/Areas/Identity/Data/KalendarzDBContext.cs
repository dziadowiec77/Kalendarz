using Kalendarz.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kalendarz.Models;
using System.Reflection.Emit;

namespace Kalendarz.Areas.Identity.Data;

public class KalendarzDBContext : IdentityDbContext<KalendarzUser, IdentityRole<int>, int>
{
    public KalendarzDBContext(DbContextOptions<KalendarzDBContext> options)
    : base(options)
    {
    }
    private class KalendarzUserEntityConfiguration : IEntityTypeConfiguration<KalendarzUser>
    {
        public void Configure(EntityTypeBuilder<KalendarzUser> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(255);
            builder.Property(x => x.LastName).HasMaxLength(255);
        }
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Entity<Kal>()
           .HasOne(k => k.TypWydarzenia)
           .WithMany(t => t.Kal)
           .HasForeignKey(k => k.TypWydarzeniaId)
           .OnDelete(DeleteBehavior.SetNull);
           

        builder.Entity<TypWydarzenia>()
            .HasMany(t => t.Kal)
            .WithOne(k => k.TypWydarzenia)
            .HasForeignKey(k => k.TypWydarzeniaId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<KalendarzUser>(entity =>
        {
            entity.ToTable(name: "AspNetUser", schema: "Security");
            entity.Property(e => e.Id).HasColumnName("AspNetUserId");

        });

        builder.Entity<IdentityRole<int>>(entity =>
        {
            entity.ToTable(name: "AspNetRole", schema: "Security");
            entity.Property(e => e.Id).HasColumnName("AspNetRoleId");

        });

        builder.Entity<IdentityUserClaim<int>>(entity =>
        {
            entity.ToTable("AspNetUserClaim", "Security");
            entity.Property(e => e.UserId).HasColumnName("AspNetUserId");
            entity.Property(e => e.Id).HasColumnName("AspNetUserClaimId");

        });

        builder.Entity<IdentityUserLogin<int>>(entity =>
        {
            entity.ToTable("AspNetUserLogin", "Security");
            entity.Property(e => e.UserId).HasColumnName("AspNetUserId");

        });

        builder.Entity<IdentityRoleClaim<int>>(entity =>
        {
            entity.ToTable("AspNetRoleClaim", "Security");
            entity.Property(e => e.Id).HasColumnName("AspNetRoleClaimId");
            entity.Property(e => e.RoleId).HasColumnName("AspNetRoleId");
        });

        builder.Entity<IdentityUserRole<int>>(entity =>
        {
            entity.ToTable("AspNetUserRole", "Security");
            entity.Property(e => e.UserId).HasColumnName("AspNetUserId");
            entity.Property(e => e.RoleId).HasColumnName("AspNetRoleId");

        });


        builder.Entity<IdentityUserToken<int>>(entity =>
        {
            entity.ToTable("AspNetUserToken", "Security");
            entity.Property(e => e.UserId).HasColumnName("AspNetUserId");

        });
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<Kalendarz.Models.Kal> Kal { get; set; } = default!;
public DbSet<TypWydarzenia> TypWydarzenia { get; set; }

}
