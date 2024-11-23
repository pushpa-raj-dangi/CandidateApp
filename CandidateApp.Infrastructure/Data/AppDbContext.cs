using CandidateApp.Domain.Entities;
using CandidateApp.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CandidateApp.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Candidate> Candidates { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CandidateConfiguration());
    }
}
