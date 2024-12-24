using BackendProject.Dto;

namespace BackendProject.Services.UserServices
{
	public interface IUserServices
	{
		Task<List<UserViewDto>> AllUser();
		Task<UserViewDto> GetUserById(int id);
		Task<bool> Blockandunblock(int userid);
	}
}
