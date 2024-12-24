using BackendProject.Dto;

namespace BackendProject.Services.ProductServices
{
	public interface IProductServices
	{
		Task<List<Productviewdto>> GetAllProducts();
		Task<Productviewdto> GetProductsById(int id);
		Task<List<Productviewdto>> GetProductsByCategory(string category);
		Task<bool> AddProduct(AddProductDto addProductDto);
		Task<bool> DeleteProduct(int id);
		Task<bool> EditProduct(int id,AddProductDto editproduct);
		Task<List<Productviewdto>> SearchProduct(string search);
		Task<List<Productviewdto>> PaginatedProduct(int pagenumber, int pagesize);
	}
}
