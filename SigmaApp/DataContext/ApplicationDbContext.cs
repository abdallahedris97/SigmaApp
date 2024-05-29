using Microsoft.EntityFrameworkCore;
using SigmaApp.Models;
using System.Collections.Generic;

namespace SigmaApp.DataContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Candidate> Candidates { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Candidate>()
            .HasKey(c => c.Email);

        modelBuilder.Entity<Candidate>()
            .Property(c => c.Email)
            .ValueGeneratedNever();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}