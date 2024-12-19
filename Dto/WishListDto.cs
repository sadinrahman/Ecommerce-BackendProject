using System.ComponentModel.DataAnnotations;

namespace BackendProject.Dto
{
	public class WishListDto
	{
		[Required]
		public int UserId { get; set; }
		[Required]
		public int ProductId { get; set; }
	}
}
