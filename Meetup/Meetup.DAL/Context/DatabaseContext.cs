using Meetup.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meetup.DAL.Context;

public class DatabaseContext : DbContext
{
    public DbSet<SpeakerEntity> Speakers { get; set; } = null!;
    public DbSet<EventEntity> Events { get; set; } = null!;

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        if (base.Database.IsRelational())
        {
            base.Database.Migrate();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SpeakerEntity>().HasIndex(s => s.Email).IsUnique();
    }
}