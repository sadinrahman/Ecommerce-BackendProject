using BackendProject.Dto;
using BackendProject.Models;

namespace BackendProject.Services.AddressServices
{
	public interface IAddressServices
	{
		Task<ApiResponses<string>> AddAdress(int userid, createaddressdto addaddress);
		Task<ApiResponses<List<ShowAddressDto>>> ShowAddresses(int userid);
		Task<bool> DeleteAddress(int userid, int addressid);
	}
}
