using DRM_Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DRM_Api.Persistance.Contexts.ModelMapping
{
	public class UserMap : BaseEntityMap<User>
	{
		public override void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("User");
			builder.Property(x => x.Username).IsRequired().HasMaxLength(50);
			builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
			builder.Property(x => x.Surname).IsRequired().HasMaxLength(30);
			builder.Property(x => x.Password).IsRequired();
			builder.Property(x => x.Email).HasMaxLength(60);
			builder.Property(x => x.UploadNo).HasDefaultValue(0);
			builder.Property(x => x.DownloadNo).HasDefaultValue(0);
			builder.Property(x => x.IsActive);
			builder.HasMany(x => x.UserRoles).WithOne(x => x.User)
				.HasForeignKey(x => x.UserId);
			builder.HasOne(x => x.RefreshToken).WithOne();
			builder.HasData(
					new User
					{
						Id = new Guid("8BDC99E6-8B9B-46A0-815C-A4A138996215"),
						Username = "admin",
						Name = "Name",
						Surname = "Surname",
						Email = "some@email.com",
						Password = "AID15NptuAnOuKL4W4TQ/FU0zNNst5ouANLHUCP1NbF+6l52j3Dbfk40LB6leJJzgg==",
						IsActive = true,
						CreatedOn = new DateTime(2020, 01, 20),
						ModifiedOn = new DateTime(2020, 01, 20),
						CreatedById = new Guid("8BDC99E6-8B9B-46A0-815C-A4A138996215"),
						ModifiedById = new Guid("8BDC99E6-8B9B-46A0-815C-A4A138996215")
					}
				);
			base.Configure(builder);
		}
	}
}
