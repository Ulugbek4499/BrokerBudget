using System.Security.Claims;

using BrokerBudget.Application.Common.Interfaces;

namespace BrokerBudget.API
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
