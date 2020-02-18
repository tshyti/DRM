using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DRM_Api.Persistance.Contexts;
using DRM_Api.Core.Repositories;
using DRM_Api.Persistance.Repositories;
using DRM_Api.Core.Services;
using DRM_Api.Services.Services;
using DRM_Api.Extensions;
using DRM_Api.Services.Mapping;
using DRM_Api.Core.UnitOfWork;
using DRM_Api.Helpers;
using DRM_Api.Middleware;
using DRM_Api.Persistance.UnitOfWork;
using DRM_Api.Services.Helpers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace DRM_Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		private readonly string corsPolicy = "MyPolicy";

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
				.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

			// Configure strongly typed settings objects
			// JWT configurations
			var tokenAppSettingsSection = Configuration.GetSection("TokenOptions");
			services.Configure<TokenAppSettings>(tokenAppSettingsSection);
			// Azure BLOB configurations
			services.Configure<BlobConfiguration>(Configuration.GetSection("BlobConfiguration"));

			// Configure JWT
			var tokenSettings = tokenAppSettingsSection.Get<TokenAppSettings>();
			var key = Encoding.UTF8.GetBytes(tokenSettings.Key);
			services.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false; // Maybe need to change this
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = true,
						ValidIssuer = tokenSettings.Issuer,
						ValidateAudience = true,
						ValidAudience = tokenSettings.Audience,
						ValidateLifetime = true,
						ClockSkew = TimeSpan.Zero
					};
				});

			// Adding CORS
			services.AddCors(options => options.AddPolicy(corsPolicy, builder =>
			{
				builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
			}));

			// Adding Entity Framework Core configuration
			services.AddDbContext<AppDbContext>(options => 
			{
				//options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
				options.UseSqlServer(Configuration.GetConnectionString("AzureConnectionString"));
			});

			// Add Swagger
			services.AddSwagger();

			// Add Redis
			services.AddDistributedRedisCache(options =>
				{
					options.Configuration = Configuration["redis:ConnectionsString"];
				});

			services.AddAutoMapper(typeof(ModelToResourceProfile));

			// Adding other services
			services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IUserFileRepository, UserFileRepository>();
			services.AddScoped<IUserFileService, UserFileService>();
			services.AddScoped<IAccessTokenManager, AccessTokenManager>();

			// Register AccessTokenManagerMiddleware
			//services.AddTransient<AccessTokenManagerMiddleware>();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();

			// Use AccessTokenManagerMiddleware
			//app.UseMiddleware<AccessTokenManagerMiddleware>();

			app.UseAuthorization();
			// Swagger
			app.AddCustomSwagger();

			// User CORS
			app.UseCors(corsPolicy);

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
