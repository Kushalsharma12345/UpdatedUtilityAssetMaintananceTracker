using Microsoft.AspNetCore.Mvc;
using WorkOrdersService.Data;
using WorkOrdersService.Models;

namespace WorkOrdersService.Controllers
{
    [ApiController]
    [Route("api/work-orders")]
    public class WorkOrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkOrdersController(AppDbContext context)
        {
            _context = context;
        }

        // POST :api/work-orders
        [HttpPost]
        public async Task<IActionResult> Create(WorkOrder workOrder)
        {
            workOrder.Status = "Open";
            _context.WorkOrders.Add(workOrder);
            await _context.SaveChangesAsync();
            return Ok(workOrder);
        }

        // GET :api/work-orders?status=Open
        [HttpGet]
        public IActionResult Get(string status)
        {
            var result = _context.WorkOrders
                                 .Where(w => w.Status == status)
                                 .ToList();
            return Ok(result);
        }

        // PUT:api/work-orders/{id}/status?status=Completed
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var workOrder = await _context.WorkOrders.FindAsync(id);
            if (workOrder == null)
                return NotFound();

            workOrder.Status = status;
            await _context.SaveChangesAsync();
            return Ok("Status updated");
        }
    }
}