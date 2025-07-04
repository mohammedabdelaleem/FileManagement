namespace FileManagement.Api.Services;

public class FileService(
	IWebHostEnvironment webHostEnvironment,
	AppDbContext context) : IFileService
{

	private readonly string uploadsPath = $"{webHostEnvironment.WebRootPath}/uploads";
	private readonly AppDbContext _context = context;

	public async Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
	{

		// fake filename and extension using Path.GetRandomFileName(); 
		var randomFileName = Path.GetRandomFileName();

		var uploadedFile = new UploadedFile
		{
			FileName = file.FileName,
			StoredFileName = randomFileName,
			ContentType = file.ContentType,
			FileExtension = Path.GetExtension(file.FileName)
		};

		// where you need to store at the server
		var path = Path.Combine(uploadsPath, randomFileName);

		// now we have the file and its path 

		// reading or streaming to copy it into the server
		using var stream = File.Create(path);
		await file.CopyToAsync(stream, cancellationToken);

		// save at db
		await _context.AddAsync(uploadedFile, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return uploadedFile.Id;
	}


}