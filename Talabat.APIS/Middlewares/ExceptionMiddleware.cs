﻿using System.Net;
using System.Text.Json;
using Talabat.APIS.Errors;

namespace Talabat.APIS.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionMiddleware> logger;
		private readonly IHostEnvironment env;

		public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment env)
        {
			this.next = next;
			this.logger = logger;
			this.env = env;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await next.Invoke(context);
			}
			catch (Exception ex)
			{

				logger.LogError(ex, ex.Message);
				context.Response.ContentType= "application/json";

				context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

				var response = env.IsDevelopment()?
					new ApiExceptionResponese((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
					:new ApiExceptionResponese((int)HttpStatusCode.InternalServerError);

				var json=JsonSerializer.Serialize(response);
				await context.Response.WriteAsync(json);
			}
		}
    }
}
