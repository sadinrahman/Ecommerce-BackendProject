using System.ComponentModel.DataAnnotations;

namespace BackendProject.Models
{
	public class Category
	{
		public int CategoryId { get; set; }
		[Required]
		public string? Name { get; set; }
		public virtual ICollection<Product>? products { get; set; }
	}
}
