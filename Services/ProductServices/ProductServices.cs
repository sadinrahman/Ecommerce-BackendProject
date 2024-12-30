using AutoMapper;
using BackendProject.AppdbContext;
using BackendProject.Dto;
using BackendProject.Models;
using BackendProject.Services.CloudinarySevice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.Services.ProductServices
{
	public class ProductServices:IProductServices
	{
		private readonly AppDbContext _Context;
		private readonly IMapper _mapper;
		private readonly ICloudinarySevice _cloudinaryService;
		public ProductServices(AppDbContext context,IMapper mapper, ICloudinarySevice cloudinaryService)
		{
			_Context = context;
			_mapper = mapper;
			_cloudinaryService = cloudinaryService;
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
						stock=x.stock,
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
					stock = products.stock,
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
					stock=x.stock,
					Image = x.Image
						
				}).ToListAsync();
			if (!products.Any())
			{
				return new List<Productviewdto>();
			}
			return products;
		}
		public async Task<bool> AddProduct( AddProductDto addProduct,IFormFile image)
		{
			try
			{
				
				if(addProduct == null)
				{
					return false;
				} 
				var category=await _Context.Category.FirstOrDefaultAsync(x=>x.CategoryId== addProduct.CategoryId);
				if(category == null)
				{
					throw new Exception("there is no  category in this id");
				}
				if (image == null)
				{
					throw new InvalidOperationException("Image is Not Uploaded");
				}
				
				string imageUrl = await _cloudinaryService.UploadImage(image);
				var product = _mapper.Map<Product>(addProduct);
				product.Image = imageUrl;
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
		public async Task<bool> EditProduct(int id,AddProductDto editproduct, IFormFile image)
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
				exproduct.stock = editproduct.stock;
				exproduct.CategoryId = editproduct.CategoryId;
				if (image != null && image.Length > 0)
				{
					string imageUrl = await _cloudinaryService.UploadImage(image);
					exproduct.Image = imageUrl;
				}
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
