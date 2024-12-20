using BackendProject.Dto;

namespace BackendProject.Services.CartService
{
	public interface ICartService
	{
		Task<bool> AddToCart(int productId, int Quantity);
		Task<List<CartViewDto>> GetCart(int userid); 
	}
}
