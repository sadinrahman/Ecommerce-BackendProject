using BackendProject.Dto;
using BackendProject.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BackendProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductServices _services;
		public ProductController(IProductServices services)
		{
			_services = services;
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetProduct()
		{
			var products=await _services.GetAllProducts();
			return Ok(products);
		}
		[HttpGet("byId/{id}")]
		[Authorize]
		public async Task<IActionResult> GetProductById(int id)
		{
			var products=await _services.GetProductsById(id);
			if (products == null)
			{
				return NotFound("There is no product exist in this id");
			}
			return Ok(products);
		}
		[HttpGet("byCategory/{CategoryName}")]
		[Authorize]
		public async Task<IActionResult> GetProductBYCategory(string CategoryName)
		{
			var products=await _services.GetProductsByCategory(CategoryName);
			if (products == null || !products.Any())
			{
				return NotFound("No products found in this category.");
			}
			return Ok(products);
		}
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> addProduct( AddProductDto addproduct)
		{
			if (addproduct == null)
			{
				return NotFound("There product is empty");
			}
			bool product=await _services.AddProduct(addproduct);
			if (product)
			{
				return Ok("Product added successfully");
			}
			return BadRequest();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			bool isdelete=await _services.DeleteProduct(id);
			if (isdelete)
			{
				return Ok("deleted successfully");
			}
			return NotFound("there is no product in this given id");
		}
		[HttpPut("Edit/{id}")]
		
		public async Task<IActionResult> EditProduct(int id,AddProductDto addproduct)
		{
			bool update=await _services.EditProduct(id, addproduct);
			if (!update)
			{
				return BadRequest();
			}
			return Ok("Product Updated successfully ");
		}
	}
}
