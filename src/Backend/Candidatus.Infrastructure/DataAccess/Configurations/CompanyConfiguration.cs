using Candidatus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candidatus.Infrastructure.DataAccess.Configurations;
public class CompanyConfiguration : EntityBaseConfiguration<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);

        builder.ToTable("companies");

        builder.Property(c => c.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne(c => c.User)
            .WithMany(u => u.Companies)
            .HasForeignKey(c => c.UserId);

        builder.HasIndex(c => c.UserId);

        builder.HasOne(c => c.City)
            .WithMany(c => c.Companies)
            .HasForeignKey(c => c.CityId);

        builder.HasIndex(c => c.CityId);
    }
}