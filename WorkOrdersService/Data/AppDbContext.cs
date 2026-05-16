using Microsoft.EntityFrameworkCore;
using WorkOrdersService.Models;

namespace WorkOrdersService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkLog> WorkLogs { get; set; }
    }
}