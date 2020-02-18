using DRM_Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DRM_Api.Persistance.Contexts.ModelMapping
{
	class UserRolesMap : BaseEntityMap<UserRoles>
	{
		public override void Configure(EntityTypeBuilder<UserRoles> builder)
		{
			builder.ToTable("UserRoles");
			builder.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.ClientSetNull);
			builder.HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.ClientSetNull);
			builder.HasData(
					new UserRoles
					{
						Id = new Guid("A662F2AB-BB52-423A-8CB1-04BE993887E3"),
						UserId = new Guid("8BDC99E6-8B9B-46A0-815C-A4A138996215"),
						RoleId = new Guid("7B68C198-2192-45E3-B908-6BB4C5AF159F"),
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
