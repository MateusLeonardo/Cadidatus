using Candidatus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candidatus.Infrastructure.DataAccess.Configurations;

public class ApplicationConfiguration : EntityBaseConfiguration<Application>
{
    public override void Configure(EntityTypeBuilder<Application> builder)
    {
        base.Configure(builder);

        builder.ToTable("applications");

        builder.Property(a => a.Title)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.Description)
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(a => a.Salary)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(a => a.WorkMode)
            .IsRequired();

        builder.Property(a => a.Url)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(a => a.ApplicationDate)
            .IsRequired();

        builder.Property(a => a.Status)
            .IsRequired();

        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId);

        builder.HasIndex(a => a.UserId);

        builder.HasOne(a => a.Company)
            .WithMany()
            .HasForeignKey(a => a.CompanyId);

        builder.HasIndex(a => a.CompanyId);

        builder.HasOne(a => a.Platform)
            .WithMany()
            .HasForeignKey(a => a.PlatformId);

        builder.HasIndex(a => a.PlatformId);
    }
}

