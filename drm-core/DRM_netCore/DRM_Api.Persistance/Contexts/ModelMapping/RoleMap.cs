using DRM_Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DRM_Api.Persistance.Contexts.ModelMapping
{
	class RoleMap : BaseEntityMap<Role>
	{
		public override void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable("Roles");
			builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
			builder.Property(x => x.IsActive);
			builder.HasData(
					new Role
					{
						Id = new Guid("7B68C198-2192-45E3-B908-6BB4C5AF159F"),
						Name = "Admin",
						IsActive = true,
						CreatedOn = new DateTime(2020, 01, 20),
						ModifiedOn = new DateTime(2020, 01, 20),
						CreatedById = new Guid("8BDC99E6-8B9B-46A0-815C-A4A138996215"),
						ModifiedById = new Guid("8BDC99E6-8B9B-46A0-815C-A4A138996215")
					},
					new Role
					{
						Id = new Guid("764D55F7-BEAB-40BE-8D62-076E5D25F01A"),
						Name = "Common",
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
