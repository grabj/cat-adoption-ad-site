using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlyCatsWebapp.Models;

namespace OnlyCatsWebapp.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>

{ 
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

    }

    public DbSet<Cat>Cats{ get; set; }
}


public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.OrganisationName).HasMaxLength(255);
        builder.Property(u => u.Website).HasMaxLength(255);
        builder.Property(u => u.Address).HasMaxLength(255);
        builder.Property(u => u.City).HasMaxLength(100);
        builder.Property(u => u.State).HasMaxLength(100);
        builder.Property(u => u.Description).HasMaxLength(1000);
    }
}
