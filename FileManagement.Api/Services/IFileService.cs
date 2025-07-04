﻿
namespace FileManagement.Api.Services;

public interface IFileService
{
	Task<Guid> UploadAsync(IFormFile file , CancellationToken cancellationToken=default);
	Task UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default);

	Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection files, CancellationToken cancellationToken = default);



}
