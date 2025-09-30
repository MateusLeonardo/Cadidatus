using Candidatus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Candidatus.Infrastructure.DataAccess;
public class CandidatusDbContext : DbContext
{
    public CandidatusDbContext(DbContextOptions<CandidatusDbContext> options) : base(options)
    {
    }

    DbSet<User> Users { get; set; }
}
