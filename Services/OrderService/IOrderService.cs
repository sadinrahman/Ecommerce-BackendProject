using BackendProject.Dto;
using BackendProject.Models;

namespace BackendProject.Services.OrderService
{
	public interface IOrderService
	{
		Task<bool> CreateOrder(int userid, CreateOrderDto addorder);
		Task<ApiResponses<OrderViewDto>> GetOrders(int userid);
		Task<List<AdminViewOrderDto>> GetOrdersforAdmin(int userid);
		Task<int> TotalProductSold();
		Task<decimal?> TotalRevenue();

	}
}
