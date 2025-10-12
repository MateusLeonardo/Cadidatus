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
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
}
