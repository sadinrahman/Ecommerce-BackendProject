﻿namespace BackendProject.Dto
{
	public class CartViewDto
	{
		public int ProductId { get; set; }
		public string? ProductName { get; set; }
		public decimal Price	{ get; set; }
		public string? Image {  get; set; }
		public decimal ProductImage { get; set; }
		public int Quantity { get; set; }

	}
}
