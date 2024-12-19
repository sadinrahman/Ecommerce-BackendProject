namespace BackendProject.Services.WishListService
{
	public interface IWishListService
	{
		Task<string> AddToWishList(int userid, int productid);
	}
}
