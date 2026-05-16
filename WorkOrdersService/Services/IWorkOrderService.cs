using WorkOrdersService.DTOs;
using WorkOrdersService.Models;

namespace WorkOrdersService.Services
{
    public interface IWorkOrderService
    {
        Task<WorkOrder> CreateWorkOrder(WorkOrderDto dto);

        Task<List<WorkOrder>> GetWorkOrders(string status);

        Task<bool> UpdateStatus(int id, string status);
    }
}