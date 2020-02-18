using System;
using System.Security.Cryptography.X509Certificates;
using DRM_Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRM_Api.Persistance.Contexts.ModelMapping
{
	public class UserFileMap : BaseEntityMap<UserFile>
	{
		public override void Configure(EntityTypeBuilder<UserFile> builder)
		{
			builder.ToTable("UserFiles");
			builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
			builder.Property(x => x.Description);
			builder.Property(x => x.URL).IsRequired();
			builder.Property(x => x.AzureName).IsRequired().HasMaxLength(250);
			builder.Property(x => x.MimeType).IsRequired().HasMaxLength(100);
			builder.Property(x => x.IsActive).HasDefaultValue(true);
			builder.HasOne(x => x.Owner)
				.WithMany(x => x.UserFiles)
				.HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.Cascade);
			builder.HasData(new UserFile
			{
				Id = new Guid("88D18A79-9BF8-4AD2-B2D7-C062EE6987E3"),
				Name = "Volcano",
				Description = "This is a volcano render.",
				URL = "https://drmprojectaccount.blob.core.windows.net/drmblob-container/Volcano 4K.png",
				AzureName = "Volcano 4K.png",
				MimeType = "image/png",
				IsActive = true,
				OwnerId = new Guid("8BDC99E6-8B9B-46A0-815C-A4A138996215"),
				CreatedById = new Guid("8BDC99E6-8B9B-46A0-815C-A4A138996215"),
				ModifiedById = new Guid("8BDC99E6-8B9B-46A0-815C-A4A138996215"),
				CreatedOn = new DateTime(2020, 01, 25),
				ModifiedOn = new DateTime(2020, 01, 25)
			});
			base.Configure(builder);
		}
	}
}