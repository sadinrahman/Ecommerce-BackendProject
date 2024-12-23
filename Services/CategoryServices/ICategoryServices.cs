using BackendProject.Dto;

namespace BackendProject.Services.CategoryServices
{
	public interface ICategoryServices
	{
		Task<bool> AddCategory(CategoryViewDto categoryViewDto);
		Task<List<CategoryViewDto>> ViewCategory();
		Task<bool> RemoveCategory(int id);
	}
}
