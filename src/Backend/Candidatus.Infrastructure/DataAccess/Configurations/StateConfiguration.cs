using Candidatus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candidatus.Infrastructure.DataAccess.Configurations;
public class StateConfiguration : EntityBaseConfiguration<State>
{
    public override void Configure(EntityTypeBuilder<State> builder)
    {
        base.Configure(builder);

        builder.ToTable("states");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(s => s.Uf)
            .IsRequired()
            .HasMaxLength(2);

        builder.HasOne(s => s.User)
            .WithMany(u => u.States)
            .HasForeignKey(s => s.UserId);
    }
}
