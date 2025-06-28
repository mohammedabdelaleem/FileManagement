
namespace FileManagement.Api.Services;

public interface IFileService
{
	Task<Guid> UploadAsync(IFormFile fileRequest, CancellationToken cancellationToken = default);
}
