using System.Security.Claims;
using GameStore.Application.Common.Interfaces;

namespace GameStore.WebApi.Services
{
    public class CurrentUser : IApplicationUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

}
