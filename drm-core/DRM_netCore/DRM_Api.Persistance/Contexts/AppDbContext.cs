using DRM_Api.Core.Entities;
using DRM_Api.Persistance.Contexts.ModelMapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DRM_Api.Persistance.Contexts
{
	public class AppDbContext : DbContext
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<UserRoles> UserRoles { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }
		public DbSet<UserFile> UserFiles { get; set; }

		public AppDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// User configuration
			builder.ApplyConfiguration(new UserMap());

			// Role configuration
			builder.ApplyConfiguration(new RoleMap());

			// UserRoles configuration
			builder.ApplyConfiguration(new UserRolesMap());

			// RefreshTokens configurations
			builder.ApplyConfiguration(new RefreshTokenMap());

			// UserFiles configurations
			builder.ApplyConfiguration(new UserFileMap());
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			string userIdString = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			Guid userId = Guid.Empty;
			if (!string.IsNullOrEmpty(userIdString))
			{
				userId = new Guid(userIdString);
			}

			if (!string.IsNullOrEmpty(userIdString))
			{
				foreach (var entry in ChangeTracker.Entries<BaseEntity>())
				{
					if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
					{
						if (entry.Entity.ModifiedOn == new DateTime())
						{
							entry.Entity.ModifiedOn = DateTime.Now; 
						}

						if (entry.State == EntityState.Added && entry.Entity.ModifiedOn == new DateTime())
						{
							entry.Entity.CreatedOn = DateTime.Now;
						}


						entry.Entity.ModifiedById = userId;

						if (entry.State == EntityState.Added)
						{
							entry.Entity.CreatedById = userId;
						}
					}
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
