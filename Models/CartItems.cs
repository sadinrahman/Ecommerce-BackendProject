﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace BackendProject.Models
{
	public class CartItems
	{

		public class OrderItems
		{
			public int Id { get; set; } 
			public int OrderId { get; set; } 
			public int ProductId { get; set; } 
			public int Quantity { get; set; }

			public virtual Order? Order { get; set; }
			public virtual Product? Product { get; set; }
		}


	}
}
