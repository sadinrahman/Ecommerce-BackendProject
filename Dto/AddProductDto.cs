﻿using System.ComponentModel.DataAnnotations;

namespace BackendProject.Dto
{
	public class AddProductDto
	{
		[Required]
		public string? Title { get; set; }
		[Required]
		public string? Description { get; set; }
		[Required]
		public decimal? Price { get; set; }
		[Required]
		public string? Image { get; set; }
		[Required]
		public int stock {  get; set; }
		[Required]
		public int CategoryId {  get; set; }
	}
}
