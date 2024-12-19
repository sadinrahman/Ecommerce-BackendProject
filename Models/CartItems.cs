﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace BackendProject.Models
{
	public class CartItems
	{
		public int Id { get; set; }
		[Required]
		public int CartId {  get; set; }
		[Required]
		public int ProductId { get; set; }
		[Required]
		public int Quantity { get; set; }
		public virtual Cart? Cart { get; set; }
		public virtual Product? Product { get; set; }

	}
}
