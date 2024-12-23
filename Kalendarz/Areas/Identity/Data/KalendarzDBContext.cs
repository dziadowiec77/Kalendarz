using Kalendarz.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalendarz.Areas.Identity.Data;

public class KalendarzDBContext : IdentityDbContext<KalendarzUser>
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
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
