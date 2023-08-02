using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Talabat.APIS.Errors;
using Talabat.APIS.Extenstions;
using Talabat.APIS.Helpers;
using Talabat.APIS.Middlewares;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIS
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddSwaggerServices();

			builder.Services.AddDbContext<StoreContext>(options=>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

			builder.Services.AddDbContext<AppIdentityDbContext> (options=> {
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			
			});
			builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
			{
				var connection = builder.Configuration.GetConnectionString("Redis");

				return  ConnectionMultiplexer.Connect(connection);
			});
			//ApplicationServicesExtentsion.AddApplicationServices(builder.Services);
			builder.Services.AddApplicationServices();


			
			builder.Services.AddIdentityServices();

            var app = builder.Build();

			using var scope=app.Services.CreateScope();

			var services=scope.ServiceProvider;
			var loogerFactory=services.GetRequiredService<ILoggerFactory>();
			try
			{

				var dbContext= services.GetRequiredService<StoreContext>();

				await dbContext.Database.MigrateAsync();
				await StoreContextSeed.SeedAsync(dbContext);
				var identityDbContext=services.GetRequiredService<AppIdentityDbContext>();
				await identityDbContext.Database.MigrateAsync();

				var userManger = services.GetRequiredService<UserManager<AppUser>>();
				await AppIdentityDbContextSeed.SeedUserAsync(userManger);


            }
			catch (Exception ex)
			{
				var logger = loogerFactory.CreateLogger<Program>();
				logger.LogError(ex, ex.Message);

				
			}

			app.UseMiddleware<ExceptionMiddleware>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddlewares();
			}
			app.UseStatusCodePagesWithRedirects("/error/{0}");
			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseStaticFiles();

			app.MapControllers();

			app.Run();
		}
	}
}