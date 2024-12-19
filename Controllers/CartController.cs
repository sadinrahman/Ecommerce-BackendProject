using BackendProject.AppdbContext;
using BackendProject.Services.CartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly AppDbContext _Context;
		private readonly ICartService _service;
		public CartController(AppDbContext context,ICartService service)
		{
			_Context = context;
			_service = service;
		}
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddtoCart(int productid)
		{
			if (HttpContext.Items["UserId"] is not int userid)
			{
				return Unauthorized("Invalid or missing user information.");
			}

			bool isAdded = await _service.AddToCart(productid, userid);
			if (isAdded)
			{
				return Ok("The product is added successfully");
			}
			return BadRequest("Invalid Product or the product is already in the cart");
		}

	}
}
