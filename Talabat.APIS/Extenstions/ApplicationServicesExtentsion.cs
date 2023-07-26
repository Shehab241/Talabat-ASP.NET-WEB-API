using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.APIS.Helpers;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.APIS.Extenstions
{
	public static class ApplicationServicesExtentsion
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));	
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


			services.AddAutoMapper(typeof(MappingProfiles));

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
														.SelectMany(P => P.Value.Errors)
														.Select(E => E.ErrorMessage)
														.ToArray();

					var vaildtionsErrorReponse = new ApiVaildationErrorResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(vaildtionsErrorReponse);
				};
			});

			return services;

		}
	}
}
