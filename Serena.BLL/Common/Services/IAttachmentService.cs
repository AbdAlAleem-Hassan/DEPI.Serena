
using Microsoft.AspNetCore.Http;

namespace Serena.BLL.Common.Services.Attachments
{
	public interface IAttachmentService
	{
		Task<string?> UploadAsync(IFormFile file, string folderName);
		bool Delete(string filePath);
	}
}
