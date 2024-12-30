namespace BackendProject.Services.CloudinarySevice
{
	public interface ICloudinarySevice
	{
		Task<string> UploadImage(IFormFile file);
	}
}
