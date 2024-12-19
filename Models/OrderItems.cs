namespace BackendProject.Models
{
	public class OrderItems
	{
		public int OrderItemId { get; set; }
		public int OrderId { get; set; }
		public int productId { get; set; }
		public decimal TotalPrice { get; set; }
		public int Quantity { get; set; }
		public virtual Product? Product { get; set; }

		public virtual Order? order { get; set; }
	}
}
