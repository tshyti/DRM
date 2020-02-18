using DRM_Api.Core.UnitOfWork;
using DRM_Api.Persistance.Contexts;
using System.Threading.Tasks;

namespace DRM_Api.Persistance.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		protected readonly AppDbContext _context;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
