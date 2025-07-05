
using FileManagement.Api.Entity;

namespace FileManagement.Api.Services;

public class FileService(
	IWebHostEnvironment webHostEnvironment,
	AppDbContext context) : IFileService
{

	private readonly string uploadsPath = $"{webHostEnvironment.WebRootPath}/uploads";
	private readonly string imagesPath = $"{webHostEnvironment.WebRootPath}/images";
	private readonly AppDbContext _context = context;

	

	public async Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
	{

		var uploadedFile = await SaveFile(file, cancellationToken);

		// save at db
		await _context.AddAsync(uploadedFile, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return uploadedFile.Id;
	}

	public async Task UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default)
	{

		// where you need to store at the server
		var path = Path.Combine(imagesPath, image.FileName);


		// reading or streaming to copy it into the server
		using var stream = File.Create(path);
		await image.CopyToAsync(stream, cancellationToken);
	}

	public async Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection files, CancellationToken cancellationToken = default)
	{
		List<UploadedFile> uploadedFiles = [];

		foreach (var file in files)
		{
			var uploadFile = await SaveFile(file, cancellationToken);	
			uploadedFiles.Add(uploadFile);
		}

		// save at db
		await _context.AddRangeAsync(uploadedFiles, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return uploadedFiles.Select(u=>u.Id).ToList();

	}

	public async Task<FileResponse?> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default)
	{
		if (await _context.Files.FindAsync(fileId) is not { } file)
			return null;

		// if the file is found at db 
		// return the file from server
		// don't forget : file stored at server using fake filename
		var path = Path.Combine(uploadsPath, file.StoredFileName); 

		MemoryStream memoryStream = new ();
		using FileStream fileStream = new FileStream(path, FileMode.Open);
		fileStream.CopyTo(memoryStream);
		memoryStream.Position = 0;

		return new FileResponse(memoryStream.ToArray() , file.ContentType, file.FileName);


	}




	private async Task<UploadedFile> SaveFile(IFormFile file, CancellationToken cancellationToken)
	{
		// fake filename and extension using Path.GetRandomFileName(); 
		var randomFileName = Path.GetRandomFileName();

		// prepare domain model
		var uploadedFile = new UploadedFile
		{
			FileName = file.FileName,
			StoredFileName = randomFileName,
			ContentType = file.ContentType,
			FileExtension = Path.GetExtension(file.FileName)
		};

		// where you need to store at the server
		var path = Path.Combine(uploadsPath, randomFileName);

		// now we have the file && its path 

		// reading or streaming to copy it into the server
		using var stream = File.Create(path);
		await file.CopyToAsync(stream, cancellationToken);

		return uploadedFile;
	}
}