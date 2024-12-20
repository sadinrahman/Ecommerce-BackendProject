﻿using BackendProject.Services.UserServices;
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
				throw new Exception(ex.Message);
			}
		}
		[HttpGet("{id}/admin")]
		[Authorize(Roles ="Admin")]
		public async Task<IActionResult> GetUserById(int id)
		{
			var user= await _Services.GetUserById(id);
			if(user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}
	}
}