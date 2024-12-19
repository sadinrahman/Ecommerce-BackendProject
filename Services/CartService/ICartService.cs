namespace BackendProject.Services.CartService
{
	public interface ICartService
	{
		Task<bool> AddToCart(int productId, int Quantity);
	}
}
