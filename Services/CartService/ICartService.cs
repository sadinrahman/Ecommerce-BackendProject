﻿using BackendProject.Dto;
using BackendProject.Models;

namespace BackendProject.Services.CartService
{
	public interface ICartService
	{
		Task<ApiResponses<CartItems>> AddToCart(int productId, int Quantity);
		Task<List<CartViewDto>> GetCart(int userid);
		Task<bool> RemoveFromCart(int userId, int productId);
	}
}
