using System.ComponentModel.DataAnnotations;

namespace BackendProject.Models
{
	public class Cart
	{
		public int Id { get; set; }
		[Required]
		public int UserId { get; set; }
		public User? User { get; set; }
		public ICollection<CartItems>? cartitems { get; set; }
	}
}
