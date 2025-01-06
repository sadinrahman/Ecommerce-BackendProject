using BackendProject.Dto;
using BackendProject.Services.LoginService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ILoginService _loginService;
		public AuthController(ILoginService loginService)
		{
				_loginService = loginService;
		}
		[HttpPost("Login")]
		public async  Task<IActionResult> Login([FromForm]  LoginDto loginDto)
		{
			var token=await _loginService.Login(loginDto);
			if(token == null)
			{
				return NotFound("user not found");
			}
			return Ok(token);
		}
		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromForm] RegisterDto  registerDto)
		{
			try
			{
				bool newuser = await _loginService.Register(registerDto);
				if (!newuser)
				{
					return BadRequest("User already exist");
				}
				return Ok("user created successfully");
			}catch (Exception ex)
			{
                return BadRequest (new Exception(ex.Message));
			}
		}
	}
}
 