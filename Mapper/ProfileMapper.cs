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
			CreateMap<AddProductDto, Product>().ReverseMap();
			CreateMap<WishListDto, WishList>().ReverseMap();
		}
	}
}
