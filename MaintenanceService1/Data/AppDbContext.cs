using MaintenanceService1.Models;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceService1.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<MaintenancePlan> MaintenancePlans => Set<MaintenancePlan>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaintenancePlan>()
            .HasKey(p => p.PlanId);

        modelBuilder.Entity<TaskItem>()
            .HasKey(t => t.TaskId);

        modelBuilder.Entity<MaintenancePlan>()
            .HasMany(p => p.Tasks)
            .WithOne(t => t.Plan)
            .HasForeignKey(t => t.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
