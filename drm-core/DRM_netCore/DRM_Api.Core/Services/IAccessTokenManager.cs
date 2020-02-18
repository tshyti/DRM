using System.Threading.Tasks;

namespace DRM_Api.Core.Services
{
	public interface IAccessTokenManager
	{
		Task<bool> IsCurrentActiveAcessTokenAsync();
		Task DeactivateCurrentAccessTokenAsync();
		Task<bool> IsActiveTokenAsync(string accessToken);
		Task DeactivateAccessTokenAsync(string accessToken);
	}
}