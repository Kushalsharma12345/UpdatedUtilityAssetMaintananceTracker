using MaintenanceService1.Data;
using MaintenanceService1.DTOs;
using MaintenanceService1.Models;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceService1.Services;

public class MaintenanceService
{
    private readonly AppDbContext _context;

    public MaintenanceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MaintenancePlan> CreateAsync(CreateMaintenancePlanDto dto)
    {
        if (dto.Tasks.Count == 0)
            throw new Exception("At least one task is required");

        if (dto.Frequency != "Monthly" && dto.Frequency != "Quarterly")
            throw new Exception("Invalid frequency");

        var plan = new MaintenancePlan
        {
            AssetId = dto.AssetId,
            Frequency = dto.Frequency,
            Tasks = dto.Tasks.Select(t => new TaskItem
            {
                Name = t.Name,
                Description = t.Description,
                EstimatedHours = t.EstimatedHours
            }).ToList()
        };

        _context.MaintenancePlans.Add(plan);
        await _context.SaveChangesAsync();

        return plan;
    }

    public async Task<List<MaintenancePlan>> GetAsync(int? assetId)
    {
        var query = _context.MaintenancePlans.Include(p => p.Tasks).AsQueryable();

        if (assetId.HasValue)
            query = query.Where(p => p.AssetId == assetId);

        return await query.ToListAsync();
    }

    public async Task<bool> UpdateAsync(int id, CreateMaintenancePlanDto dto)
    {
        var plan = await _context.MaintenancePlans
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.PlanId == id);

        if (plan == null) return false;

        plan.AssetId = dto.AssetId;
        plan.Frequency = dto.Frequency;

        _context.Tasks.RemoveRange(plan.Tasks);

        plan.Tasks = dto.Tasks.Select(t => new TaskItem
        {
            Name = t.Name,
            Description = t.Description,
            EstimatedHours = t.EstimatedHours
        }).ToList();

        await _context.SaveChangesAsync();
        return true;
    }
}