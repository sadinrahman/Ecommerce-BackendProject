﻿namespace BackendProject.Dto
{
	public class OrderItemDto
	{
		public string? ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal? TotalPrice { get; set; }
	}
}
