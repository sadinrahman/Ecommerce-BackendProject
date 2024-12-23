﻿using BackendProject.Dto;
using BackendProject.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryServices _services;
		public CategoryController(ICategoryServices services)
		{
			_services = services;
		}
		[HttpPost]
		[Authorize(Roles ="Admin")]
		public async Task<IActionResult> AddCategory(CategoryViewDto categoryViewDto)
		{
			try
			{
				var res = await _services.AddCategory(categoryViewDto);
				if (res)
				{
					return Ok("Category successfully added");
				}
				return Conflict("The category already exist");

			}
			catch (Exception ex)
			{
				return BadRequest( new Exception(ex.Message));
			}
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetCategory()
		{
			try
			{
				var categories = await _services.ViewCategory();
				return Ok(categories);
			}catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			try
			{
				var res = await _services.RemoveCategory(id);
				if (res)
				{
					return Ok("Category deleted succedully");
				}
				return NotFound("Category Not found");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
