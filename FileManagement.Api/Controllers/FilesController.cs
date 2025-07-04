using FileManagement.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FileManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FilesController(
	IFileService fileService
	) : ControllerBase
{
	private readonly IFileService _fileService = fileService;

	[HttpPost("upload")]
	public async Task<IActionResult> Upload([FromForm] UploadedFileRequest request, CancellationToken cancellationToken = default)
	{
		var fileId = await _fileService.UploadAsync(request.File, cancellationToken);
		return Created();
	}


	[HttpPost("upload-many")]
	public async Task<IActionResult> UploadMany([FromForm] UploadManyFilesRequest request, CancellationToken cancellationToken = default)
	{
		var filesIds = await _fileService.UploadManyAsync(request.Files, cancellationToken);

		return Ok(filesIds);
	}


	[HttpPost("upload-image")]
	public async Task<IActionResult> UploadImage([FromForm] UploadedImageRequest request, CancellationToken cancellationToken = default)
	{
		await _fileService.UploadImageAsync(request.Image, cancellationToken);

		return Created();
	}
}
