using AutoMapper;
using BackendProject.AppdbContext;
using BackendProject.Dto;
using BackendProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.Services.CartService
{
	public class CartService:ICartService
	{
		private readonly AppDbContext _context;
		private readonly ILogger<CartService> _logger;
		private readonly IMapper _mapper;
		public CartService(AppDbContext context,ILogger<CartService> logger,IMapper mapper)
		{
			_context = context;
			_logger = logger;
			_mapper = mapper;
		}
		public async Task<bool> AddToCart(int productId,int userid)
		{
			try
			{
				var isuser=await _context.users.Include(ci=>ci.Cart).ThenInclude(x=>x.cartitems).FirstOrDefaultAsync(x=>x.Id==userid);
				if(isuser == null )
				{
					return false;
				}
				var isproduct=await _context.products.FirstOrDefaultAsync(x=>x.ProductId==productId);
				if(isproduct == null)
				{
					return false;
				}
				if (isuser.Cart == null)
				{
					isuser.Cart = new Cart()
					{
						UserId = userid,
						cartitems = new List<CartItems>()
					};
					await _context.carts.AddAsync(isuser.Cart);
					await _context.SaveChangesAsync();
				}
				if (isproduct?.stock <= 0)
				{
					return false;
				}
				var check= isuser.Cart?.cartitems?.FirstOrDefault(p=>p.ProductId==productId);
				if (check != null)
				{
					check.Quantity++;
					await _context.SaveChangesAsync();

				}
				else
				{

					var item = new CartItems
					{
						CartId = isuser.Cart.Id,
						ProductId = productId,
						Quantity = 1
					};
					isuser?.Cart?.cartitems?.Add(item);
				}
				await _context.SaveChangesAsync();
				return true;
				
			}catch (Exception ex)
			{
				_logger.LogError("Error adding to cart: " + ex.Message);
				if (ex.InnerException != null)
				{
					_logger.LogError("Inner exception: " + ex.InnerException.Message);
				}
				return false;
			}
		}
		public async Task<List<CartViewDto>> GetCart(int userid)
		{
			if(userid == 0)
			{
				throw new Exception("Userid is null");
			}
			var user=await _context.carts.Include(c=>c.cartitems).ThenInclude(p=>p.Product).FirstOrDefaultAsync(x=>x.UserId==userid);
			if (user != null)
			{
				return _mapper.Map<List<CartViewDto>>(user.cartitems);
			}
			return new List<CartViewDto>();
		}

	}
}
