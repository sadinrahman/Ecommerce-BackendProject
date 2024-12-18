namespace BackendProject.Dto
{
	public class AddProductDto
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public decimal? Price { get; set; }
		public string? Image { get; set; }
		public int stock {  get; set; }
		public int CategoryId {  get; set; }
	}
}
