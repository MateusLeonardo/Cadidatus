using Candidatus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candidatus.Infrastructure.DataAccess.Configurations;

public class PlatformConfiguration : EntityBaseConfiguration<Platform>
{
    public override void Configure(EntityTypeBuilder<Platform> builder)
    {
        base.Configure(builder);

        builder.ToTable("platforms");

        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Url)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne(p => p.User)
            .WithMany(u => u.Platforms)
            .HasForeignKey(p => p.UserId);

        builder.HasIndex(p => p.UserId);
    }
}