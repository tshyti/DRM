using System.Threading.Tasks;

namespace DRM_Api.Core.UnitOfWork
{
	public interface IUnitOfWork
	{
		Task<int> SaveChangesAsync();
	}
}
