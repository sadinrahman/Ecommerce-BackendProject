using BackendProject.Dto;
using BackendProject.Services.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _service;
		public OrderController(IOrderService service)
		{
			_service = service;
		}
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> OrderItem(CreateOrderDto CreateOrder)
		{
			try
			{
				var userId = Convert.ToInt32(HttpContext.Items["UserId"]);
				var Order = await _service.CreateOrder(userId, CreateOrder);
				return Ok("Products Ordered");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetOrders()
		{
			try
			{
				var userId = Convert.ToInt32(HttpContext.Items["UserId"]);
				var result = await _service.GetOrders(userId);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
