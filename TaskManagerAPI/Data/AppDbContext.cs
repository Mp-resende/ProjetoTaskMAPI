using Microsoft.EntityFrameworkCore;

namespace TaskManagerAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Models.TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.TaskEntity>().ToTable("tasks");

        modelBuilder.Entity<Models.TaskEntity>()
      .Property(e => e.Status)
      .HasConversion<string>()
      .HasDefaultValue(Models.TaskStatus.Pending)
      .IsRequired();
    }
}
