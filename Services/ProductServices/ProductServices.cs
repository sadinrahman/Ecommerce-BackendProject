using AutoMapper;
using BackendProject.AppdbContext;
using BackendProject.Dto;
using BackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.Services.ProductServices
{
	public class ProductServices:IProductServices
	{
		private readonly AppDbContext _Context;
		private readonly IMapper _mapper;
		public ProductServices(AppDbContext context,IMapper mapper)
		{
			_Context = context;
			_mapper = mapper;
		}
		public async  Task<List<Productviewdto>> GetAllProducts()
		{
			try
			{
				var products = await _Context.products.Include(x=>x.category).ToListAsync();
				if (products.Count > 0)
				{
					var productall = products.Select(x => new Productviewdto
					{
						Title = x.Title,
						Description = x.Description,
						Price = x.Price,
						Image = x.Image
					}).ToList();
					return productall;
				}
				return new List<Productviewdto>();
			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public async Task<Productviewdto> GetProductsById(int id)
		{
			try
			{
				var products =await _Context.products.FirstOrDefaultAsync(x => x.ProductId == id);
				if(products == null)
				{
					return null;
				}
				return new Productviewdto()
				{
					Title = products.Title,
					Description = products.Description,
					Price = products.Price,
					Image = products.Image
				};

			}catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public async Task<List<Productviewdto>> GetProductsByCategory(string categoryname)
		{
			var products = await _Context.products.Include(x => x.category)
				.Where(x => x.category.Name == categoryname)
				.Select(x => new Productviewdto
				{
					Title = x.Title,
					Description = x.Description,
					Price = x.Price,
					Image = x.Image
						
				}).ToListAsync();
			if (!products.Any())
			{
				return new List<Productviewdto>();
			}
			return products;
		}
		public async Task<bool> AddProduct( AddProductDto addProduct)
		{
			try
			{
				var product = _mapper.Map<Product>(addProduct);
				if(product == null)
				{
					return false;
				}
				await _Context.products.AddAsync(product);
				await _Context.SaveChangesAsync();
				return true;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public async Task<bool> DeleteProduct(int id)
		{
			var product= await _Context.products.FirstOrDefaultAsync(x=>x.ProductId == id);
			if(product == null)
			{
				return false;
			}
			 _Context.products.Remove(product);
			await _Context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> EditProduct(int id,AddProductDto editproduct)
		{
			var exproduct= await _Context.products.FirstOrDefaultAsync(x=>x.ProductId==id);
			var catexist=await _Context.Category.FirstOrDefaultAsync(x=>x.CategoryId==editproduct.CategoryId);
			if (catexist == null)
			{
				throw new Exception("There is no category in this id");
			}
			if(exproduct == null)
			{
				return false;
			}
			try
			{
				exproduct.Title = editproduct.Title;
				exproduct.Description = editproduct.Description;
				exproduct.Price = editproduct.Price;
				exproduct.Image = editproduct.Image;
				exproduct.stock = editproduct.stock;
				exproduct.CategoryId = editproduct.CategoryId;
				_Context.products.Update(exproduct);
				await _Context.SaveChangesAsync();
				return true;
			}catch (Exception ex)
			{
				throw new Exception(ex.Message);
				
			}
		}
		public async Task<List<Productviewdto>> SearchProduct(string search)
		{
			if (string.IsNullOrEmpty(search))
			{
				return new List<Productviewdto> ();
			}
			var products = await _Context.products.Include(x => x.category)
				.Where(p => p.Title.ToLower().Contains(search.ToLower()))
				.ToListAsync();
			return products.Select(s => new Productviewdto
			{
				Title = s.Title,
				Description = s.Description,
				Price = s.Price,
				Image = s.Image,
			}).ToList();
		}
		public async Task<List<Productviewdto>> PaginatedProduct(int pagenumber,int pagesize)
		{
			try
			{	
				var products=await _Context.products.Include (x => x.category).Skip((pagenumber-1)*pagesize).Take(pagesize).ToListAsync();
				return products.Select(p => new Productviewdto
				{
					Title = p.Title,
					Description = p.Description,
					Price = p.Price,
					Image = p.Image,
				}).ToList();
			}catch (Exception ex)
			{
				throw new Exception (ex.Message);
			}
		}
	}
}
