﻿
namespace FileManagement.Api.Services;

public interface IFileService
{
	Task<Guid> UploadAsync(IFormFile file , CancellationToken cancellationToken=default);
}
