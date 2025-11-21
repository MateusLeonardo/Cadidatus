using Candidatus.Domain.Entities;
using Candidatus.Infrastructure.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Candidatus.Infrastructure.DataAccess;
public class CandidatusDbContext : DbContext
{
    public CandidatusDbContext(DbContextOptions<CandidatusDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new StateConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new PlatformConfiguration());
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Platform> Platforms { get; set; }
}
