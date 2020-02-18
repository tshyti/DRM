using System;
using System.Collections.Generic;
using System.Text;

namespace DRM_Api.Core.Entities
{
	public class BaseEntity
	{
		public Guid Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime ModifiedOn { get; set; }
		public Guid CreatedById { get; set; }
		public User CreatedBy { get; set; }
		public Guid ModifiedById { get; set; }
		public User ModifiedBy { get; set; }
	}
}
