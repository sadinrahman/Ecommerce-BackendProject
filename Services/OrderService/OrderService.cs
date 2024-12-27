using BackendProject.AppdbContext;
using BackendProject.Dto;
using BackendProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BackendProject.Services.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly AppDbContext _context;
		public OrderService(AppDbContext context)
		{
			_context = context;
		}
		public async Task<bool> CreateOrder(int userid, CreateOrderDto addorder)
		{
			var usercart = await _context.carts.Include(x => x.cartitems).ThenInclude(y => y.Product).FirstOrDefaultAsync(p => p.UserId == userid);
			if (usercart == null || usercart.cartitems == null)
			{
				return false;
			}
			decimal? totalAmount = usercart.cartitems
								  .Where(item => item.Product != null)
								  .Sum(item => item.Product.Price * item.Quantity);
			var neworder = new Order
			{
				UserId = userid,
				OrderDate = DateTime.Now,
				AddressId = addorder.AddressId,
				TotalAmount = totalAmount,
				TransactionId = addorder.TransactionId,
				OrderItems = usercart.cartitems.Select(item =>
			   new OrderItems
			   {
				   productId = item.ProductId,
				   Quantity = item.Quantity,
				   TotalPrice = (item.Product.Price * item.Quantity)

			   }).ToList()
			};
			await _context.Orders.AddAsync(neworder);
			_context.carts.Remove(usercart);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<ApiResponses<OrderViewDto>> GetOrders(int userid)
		{
			var userorder = await _context.Orders.Include(x => x.OrderItems).ThenInclude(i => i.Product).FirstOrDefaultAsync(x => x.UserId == userid);
			if (userorder == null || userorder.OrderItems == null)
			{
				return new ApiResponses<OrderViewDto>(200, "Order List is Empty");
			}
			var resOrder = userorder.OrderItems.Select(item => new OrderItemDto
			{
				ProductName = item.Product.Title,
				Quantity = item.Quantity,
				TotalPrice = (item.Product.Price * item.Quantity)

			}).ToList();
			decimal? totalAmount = userorder.OrderItems
								  .Where(item => item.Product != null)
								  .Sum(item => item.Product.Price * item.Quantity);
			var orderaddress = await _context.Addresses.FirstOrDefaultAsync(x => x.AddressId == userorder.AddressId);
			var res = new OrderViewDto
			{
				TransactionId = userorder.TransactionId,
				TotalAmount = totalAmount,
				DeliveryAdrress = $"{orderaddress.FullName},{orderaddress.Pincode} {orderaddress.HouseName},{orderaddress.Place},{orderaddress.PostOffice},{orderaddress.LandMark}",
				Phone = orderaddress.PhoneNumber,
				OrderDate = userorder.OrderDate,
				Items = resOrder,


			};
			return new ApiResponses<OrderViewDto>(200, "Successfully Fetched User Cart", res);

		}
	}
}
