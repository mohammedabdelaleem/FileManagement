namespace FileManagement.Api.Services;

public class FileService(
	IWebHostEnvironment webHostEnvironment,
	AppDbContext context) : IFileService
{

	public readonly string _filesPath = $"{webHostEnvironment.WebRootPath}/uploads";
	private readonly AppDbContext _context = context;

	public async Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken)
	{
		
		string randomFileName = Path.GetRandomFileName();

		UploadedFile uploadedFile = new UploadedFile()
		{
			FileName = file.FileName,
			ContentType = file.ContentType,
			FileExtension = Path.GetExtension(file.FileName),
			StoredFileName = randomFileName
		};

		string path = Path.Combine(_filesPath, randomFileName);


		using var stream = File.Create(path);
		await file.CopyToAsync(stream, cancellationToken);


		await _context.AddAsync(uploadedFile,cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return uploadedFile.Id;	
	}
}
