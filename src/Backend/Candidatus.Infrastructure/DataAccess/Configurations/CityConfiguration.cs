using Candidatus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candidatus.Infrastructure.DataAccess.Configurations;

public class CityConfiguration : EntityBaseConfiguration<City>
{
    public override void Configure(EntityTypeBuilder<City> builder)
    {
        base.Configure(builder);

        builder.ToTable("cities");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne(c => c.State)
            .WithMany(s => s.Cities)
            .HasForeignKey(c => c.StateId);

        builder.HasOne(c => c.User)
            .WithMany(s => s.Cities)
            .HasForeignKey(c => c.UserId);
    }
}
