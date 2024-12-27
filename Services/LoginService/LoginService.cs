using AutoMapper;
using BackendProject.AppdbContext;
using BackendProject.Dto;
using BackendProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendProject.Services.LoginService
{
	public class LoginService : ILoginService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		private readonly ILogger<LoginService> _logger;
		private readonly IConfiguration _configuration;
		public LoginService(AppDbContext context, IMapper mapper, ILogger<LoginService> logger, IConfiguration configuration)
		{
			_context = context;
			_mapper = mapper;
			_logger = logger;
			_configuration = configuration;
		}
		public async Task<bool> Register(RegisterDto newUser)
		{
			try
			{
				if (newUser == null)
				{
					throw new ArgumentNullException("User data cannnot be null");
				}
				var IsUserexist = await _context.users.SingleOrDefaultAsync(u => u.Email == newUser.Email);
				if (IsUserexist != null)
				{
					return false;
				}

				newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

				var user = _mapper.Map<User>(newUser);
				_context.users.Add(user);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (ArgumentNullException ex)
			{
				throw new Exception($"Validation error: {ex.Message}", ex);
			}
			catch (DbUpdateException ex)
			{
				throw new Exception($"database error:{ex.InnerException?.Message ?? ex.Message}");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		public async Task<resultDto> Login(LoginDto userdto)
		{
			try
			{
				_logger.LogInformation("Logging into the user");

				var usr = await _context.users.FirstOrDefaultAsync(u => u.UserName == userdto.UserName);
				if (usr == null)
				{
					_logger.LogWarning("user not found");
					return new resultDto { Error = "Not Found" };
				}
				if (usr.IsBlocked == true)
				{
					_logger.LogWarning("user is blocked");
					return new resultDto { Error = "user is blocked" };
				}

				_logger.LogInformation("validating email...");
				var pass = ValidatePassword(userdto.Password, usr.Password);

				if (!pass)
				{
					_logger.LogWarning("invalid password");
					return new resultDto { Error = "Invalid Password" };
				}

				_logger.LogInformation("generating token");
				var token = GenerateToken(usr);
				return new resultDto 
				{
					Token = token,
					Role = usr.Role,
					Email = usr.Email,
					Id = usr.Id,
					Name = usr.UserName

				};
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error in login:{ex.Message}");
				throw;
			}
		}
		public string GenerateToken(User user)
		{

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>
	{
		new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
        new Claim(ClaimTypes.Name, user.UserName), 
        new Claim(ClaimTypes.Role, user.Role) 
    };

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddHours(1),
				Issuer = "YourIssuer",
				Audience = "YourAudience",
				SigningCredentials = creds 
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
		private bool ValidatePassword(string password, string hashPassword)
		{
			return BCrypt.Net.BCrypt.Verify(password, hashPassword);
		}
	}
	}
