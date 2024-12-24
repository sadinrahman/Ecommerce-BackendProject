using AutoMapper;
using BackendProject.AppdbContext;
using BackendProject.Dto;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace BackendProject.Services.UserServices
{
	public class UserServices:IUserServices
	{
		private readonly AppDbContext _Context;
		private readonly IMapper _mapper;
		public UserServices(AppDbContext context,IMapper mapper)
		{
			_Context = context;
			_mapper = mapper;
		}
		public async Task<List<UserViewDto>> AllUser()
		{
			try
			{
				var users=await _Context.users.ToListAsync();
				if (users.Count > 0)
				{
					var user=_mapper.Map<List<UserViewDto>>(users);
					return user;
				}
				return new List<UserViewDto>();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public async Task<UserViewDto> GetUserById(int id)
		{
			var user=await _Context.users.SingleOrDefaultAsync(x => x.Id == id);
			if(user == null)
			{
				return null;
			}
			return _mapper.Map<UserViewDto>(user);
		}
		public async Task<bool> Blockandunblock(int userid)
		{
			var user= await _Context.users.SingleOrDefaultAsync(u=>u.Id == userid);
			if(user == null)
			{
				return false;
			}
			user.IsBlocked = !user.IsBlocked;
			await _Context.SaveChangesAsync();
			return true;
		}
	}
}
