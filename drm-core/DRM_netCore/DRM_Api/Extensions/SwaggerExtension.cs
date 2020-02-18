using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace DRM_Api.Extensions
{
	public static class SwaggerExtension
	{
		public static void AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(configuration =>
			{
				configuration.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "DRM_Api", Version = "v1" });

				var securityScheme = new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Description = "Enter JWT Bearer authorization token",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer", // Must be lowercase
					BearerFormat = "Bearer {token}",
					Reference = new OpenApiReference
					{
						Id = JwtBearerDefaults.AuthenticationScheme,
						Type = ReferenceType.SecurityScheme
					}
				};

				configuration.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
				configuration.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{securityScheme, Array.Empty<string>() }
				});

				// Set the comment path for the Swagger JSON and UI
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				configuration.IncludeXmlComments(xmlPath);
			});
		}

		public static void AddCustomSwagger(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(configuration =>
			{
				configuration.SwaggerEndpoint("/swagger/v1/swagger.json", "DRM_Api v1");
				configuration.RoutePrefix = string.Empty;
			});
		}
	}
}
