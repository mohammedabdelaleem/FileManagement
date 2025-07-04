﻿using FileManagement.Api.Services;
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
		return CreatedAtAction(nameof(Download), new { id = fileId }, null);
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

	[HttpPost("upload-video")]
	public async Task<IActionResult> UploadVideo([FromForm] UploadVideoRequest request, CancellationToken cancellationToken = default)
	{
		var videoId= await _fileService.UploadVideoAsync(request.Video, cancellationToken);

		return CreatedAtAction(nameof(Stream) ,new {id = videoId} , null);
	}

	[HttpGet("download/{id}")]
	public async Task<IActionResult> Download([FromRoute] Guid id, CancellationToken cancellationToken = default)
	{
		var response =  await _fileService.DownloadAsync(id, cancellationToken);

		return response is not null ? File(response.FileContent, response.ContentType,response.FileName): NotFound();
	}

	[HttpGet("stream/{id}")]
	public async Task<IActionResult> Stream([FromRoute] Guid id, CancellationToken cancellationToken = default)
	{
		var (stream, contentType,fileName) = await _fileService.StreamAsync(id, cancellationToken);

		return stream is not null ? File(stream, contentType, fileName,enableRangeProcessing:true) : NotFound();
	}
}
