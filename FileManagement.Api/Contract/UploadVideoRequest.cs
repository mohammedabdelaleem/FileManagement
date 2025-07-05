namespace FileManagement.Api.Contract;

public record UploadVideoRequest
	(
		IFormFile Video
	);