using AutoMapper;
using BackendProject.Dto;
using BackendProject.Models;

namespace BackendProject.Mapper
{
	public class ProfileMapper:Profile
	{
		public ProfileMapper()
		{
			CreateMap<User, LoginDto>().ReverseMap();
			CreateMap<User,RegisterDto>().ReverseMap();
			CreateMap<User,UserViewDto>().ReverseMap();
			CreateMap<AddProductDto, Product>().ReverseMap();
			CreateMap<WishListDto, WishList>().ReverseMap();
			CreateMap<Category, CategoryViewDto>().ReverseMap();
			CreateMap<CartItems, CartViewDto>().ReverseMap();
			CreateMap<Address,createaddressdto>().ReverseMap();
			CreateMap<Address,ShowAddressDto>().ReverseMap();
			CreateMap<Order,OrderViewDto>().ReverseMap();
		}
	}
}
