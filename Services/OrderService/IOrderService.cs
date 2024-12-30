using BackendProject.Dto;
using BackendProject.Models;

namespace BackendProject.Services.OrderService
{
	public interface IOrderService
	{
		Task<bool> CreateOrder(int userid, CreateOrderDto addorder);
		Task<ApiResponses<List<OrderViewDto>>> GetOrders(int userid);
		Task<List<AdminViewOrderDto>> GetOrdersforAdmin(int userid);
		Task<int> TotalProductSold();
		Task<decimal?> TotalRevenue();
		Task<ApiResponses<string>> UpdateOrderStatus(int orderId, string newStatus);

	}
}
