﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BackendProject.Services.CloudinarySevice
{
	public class CloudinarySevice: ICloudinarySevice
	{
		private readonly Cloudinary _cloudinary;
		public CloudinarySevice(IConfiguration configuration) 
		{
			var cloudName = configuration["CloudinarySettings:CloudName"];
			var apiKey = configuration["CloudinarySettings:ApiKey"];
			var apiSecret = configuration["CloudinarySettings:ApiSecret"];

			
			var account = new Account(cloudName, apiKey, apiSecret);
			_cloudinary = new Cloudinary(account);
		}
		public async Task<string> UploadImage(IFormFile file)
		{
			if (file == null || file.Length == 0) return null;

			using (var stream = file.OpenReadStream())
			{
				var uploadParams = new ImageUploadParams
				{
					File = new FileDescription(file.FileName, stream),
					Transformation = new Transformation().Height(500).Width(500).Crop("fill")
				};
				var uploadResult = await _cloudinary.UploadAsync(uploadParams);
				return uploadResult.SecureUrl.ToString();
			}
		}
	}
}
