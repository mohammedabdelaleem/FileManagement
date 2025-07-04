namespace FileManagement.Api.Contract;

public record UploadedImageRequest(
	IFormFile Image
	);