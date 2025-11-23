using Microsoft.AspNetCore.Http;

namespace Serena.BLL.Common.Services.Attachments
{
	public class AttachmentService : IAttachmentService
	{
		private readonly List<string> _allowedExtensions = new() { ".jpg", ".jpeg", ".png" };
		private const int _maxFileSizeInBytes = 2_097_152; // 2 MB
		public async Task<string?> UploadAsync(IFormFile file, string folderName)
		{
			var extension = Path.GetExtension(file.FileName);
			if (!_allowedExtensions.Contains(extension))
				return null;
			if (file.Length > _maxFileSizeInBytes)
				return null;

			var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
			if (!Directory.Exists(FolderPath))
				Directory.CreateDirectory(FolderPath);

			var fileName = $"{Guid.NewGuid()}{extension}";

			var filePath = Path.Combine(FolderPath, fileName);

			using var fileStream = new FileStream(filePath, FileMode.Create);
			await file.CopyToAsync(fileStream);
			
			return fileName;
		}
		public bool Delete(string filePath)
		{
			if(File.Exists(filePath))
			{
				File.Delete(filePath);
				return true;
			}
			return false;
		}

	}
}
