using System.ComponentModel.DataAnnotations;

namespace BackendProject.Dto
{
	public class CategoryViewDto
	{
		public int CategoryId { get; set; }
		
		public string? Name { get; set; }
	}
}
