using BackendProject.Dto;
using BackendProject.Models;
using BackendProject.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private IUserServices _Services;
		public UserController(IUserServices services)
		{
			_Services = services;
		}
		[HttpGet("admin")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAll()
		{
			try 
			{
				var users = await _Services.AllUser();
				return Ok(users);

			}catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		[HttpGet("{id}/admin")]
		[Authorize(Roles ="Admin")]
		public async Task<IActionResult> GetUserById(int id)
		{
			var user= await _Services.GetUserById(id);
			if (user == null)
				return NotFound(new ApiResponses<string>(404, "User not found", null));

			var res = new ApiResponses<UserViewDto>(200, "Fetched user by id", user);
			return Ok(res);
		}
		[HttpPatch("{id}/blockunblock")]
		[Authorize("Admin")]
		public async Task<IActionResult> BlockorUnblock(int id)
		{
			try
			{
				bool isblocked =await _Services.Blockandunblock(id);
				return Ok(isblocked);
			}catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
