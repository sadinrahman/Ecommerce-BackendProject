﻿using System.ComponentModel.DataAnnotations;

namespace BackendProject.Models
{
	public class User
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="Name is required")]
		public string? UserName { get; set; }
		[Required]
		[EmailAddress(ErrorMessage ="Enter proper Email")]
		public string? Email { get; set; }
		[Required]
		[MinLength(6,ErrorMessage="Password must be above 6 characters")]
		[RegularExpression(@"^(?=.[A-Za-z])(?=.\d)(?=.[@$!%?&])[A-Za-z\d@$!%*?&]{8,}$",
		 ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
		public string? Password { get; set; }
		
		[Compare("Password",ErrorMessage = "password and confirmation password do not match.")]
		public string? ConformPassword { get; set; }
		public string? Role { get; set; }
		public virtual Cart? Cart { get; set; }
		public	virtual List<WishList>? WishList { get; set; }
		public virtual List<Order>? Orders { get; set; }
		public ICollection<Address>? Addresses { get; set; }

	}
}
