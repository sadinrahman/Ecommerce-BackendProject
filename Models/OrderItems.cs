namespace BackendProject.Models
{
	public class OrderItems
	{
		public int OrderItemsId { get; set; }
		public int OrderId { get; set; }
		public int productId { get; set; }
		public decimal? TotalPrice { get; set; }
		public int Quantity { get; set; }
		public virtual Product? Product { get; set; }

		public virtual Order? Order { get; set; }
	}
}
