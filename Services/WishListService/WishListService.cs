using AutoMapper;
using BackendProject.AppdbContext;
using BackendProject.Dto;
using BackendProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.Services.WishListService
{
	public class WishListService:IWishListService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		public WishListService(AppDbContext context,IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<string> AddToWishList(int userid,int productid)
		{
			var isexist=await _context.wishList.Include(p=>p.products).FirstOrDefaultAsync(w=>w.ProductId==productid && w.UserId==userid);
			if (isexist==null)
			{
				WishListDto wishListDto = new WishListDto()
				{
					ProductId = productid,
					UserId = userid,
				};
				var wish = _mapper.Map<WishList>(wishListDto);
				_context.wishList.Add(wish);
				await _context.SaveChangesAsync();
				return "item added To whish list";
			}
			else
			{
				return "item already in the wishlist";
			}
		}
	}
}
