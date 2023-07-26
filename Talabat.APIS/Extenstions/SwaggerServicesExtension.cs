namespace Talabat.APIS.Extenstions
{
	public static class SwaggerServicesExtension
	{
		public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			return services;
		}

		public static WebApplication UseSwaggerMiddlewares(this WebApplication application)
		{
			application.UseSwagger();
			application.UseSwaggerUI();
			return application;
		}
	}
}
