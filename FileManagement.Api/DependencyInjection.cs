
namespace FileManagement.Api;

public static class DependencyInjection
{
	public static IServiceCollection AddDependencies (this IServiceCollection services, IConfiguration configuration)
	{
		services.AddControllers();
		services.AddOpenApi();

		services.AddDatabaseConfig (configuration);

		return services;
	}
		
	private static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
	{
		var constr = configuration.GetConnectionString("constr") ??
			throw new InvalidOperationException("There is no Connection String For The 'DefaultConStr' Key ");

		services.AddDbContext<AppDbContext>(options =>
		{
			options.UseSqlServer(constr);
		});

		return services;	
	}

}
