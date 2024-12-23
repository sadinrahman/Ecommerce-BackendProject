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
		public async Task<bool> RemoveFromWishlist(int userid, int productid)
		{
			var isexist = await _context.wishList.Include(p => p.products).FirstOrDefaultAsync(w => w.ProductId == productid && w.UserId == userid);
			if (isexist != null)
			{
				
				_context.wishList.Remove(isexist);
				await _context.SaveChangesAsync();
				return true;
			}
			else
			{
				return false;
			}
		}
		public async Task<List<WishListViewDto>> GetWishList(int userId)
        {
            try
            {
                var items = await _context.wishList.Include(p => p.products)
                    .ThenInclude(c => c.category)
                    .Where(c => c.UserId == userId).ToListAsync();

                if (items != null)
                {
                    var p = items.Select(w => new WishListViewDto
                    {
                        Id = w.Id,
                        ProductId=w.products.ProductId,
                        ProductName = w.products.Title,
                        ProductDescription = w.products.Description,
                        Price = w.products.Price,
                        ProductImage = w.products.Image, 
                        CategoryName = w.products.category.Name
                    }).ToList();

                    return p;
                }
                else
                {
                    return new List<WishListViewDto>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
