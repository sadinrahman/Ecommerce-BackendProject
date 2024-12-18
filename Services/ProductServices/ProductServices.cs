using AutoMapper;
using BackendProject.AppdbContext;
using BackendProject.Dto;
using BackendProject.Models;
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
		public async Task<bool> AddProduct(AddProductDto addProduct)
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
				Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
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
		
	}
}
