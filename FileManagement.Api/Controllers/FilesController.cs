using FileManagement.Api.Services;
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
	public async Task<IActionResult> Upload([FromForm] UploadedFileRequest request , CancellationToken cancellationToken = default)
	{
		var fileId = await _fileService.UploadAsync(request.File, cancellationToken);

		return Created();
	}

}
