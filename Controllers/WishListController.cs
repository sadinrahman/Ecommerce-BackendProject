using BackendProject.Services.WishListService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WishListController : ControllerBase
	{
		private readonly IWishListService _service;
		public WishListController(IWishListService service)
		{
			_service = service;
		}
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddtoWishlist(int productid)
		{
			int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
			string isadded=await _service.AddToWishList(userId, productid);
			if(isadded== "item added To whish list")
			{
				return Ok("The product is added to wishlist");
			}
			else
			{
				return BadRequest("Wrong item or item already in the wishlist");
			}
		}
	}
}
