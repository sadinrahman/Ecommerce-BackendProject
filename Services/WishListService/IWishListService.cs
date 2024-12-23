﻿using BackendProject.Dto;

namespace BackendProject.Services.WishListService
{
	public interface IWishListService
	{
		Task<string> AddToWishList(int userid, int productid);
		Task<bool> RemoveFromWishlist(int userid, int productid);
		Task<List<WishListViewDto>> GetWishList(int userId);
	}
}
