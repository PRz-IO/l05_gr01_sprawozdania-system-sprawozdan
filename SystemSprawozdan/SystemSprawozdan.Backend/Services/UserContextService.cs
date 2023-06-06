using System.Security.Claims;
using SystemSprawozdan.Shared.Enums;

namespace SystemSprawozdan.Backend.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
        UserRoleEnum? GetUserRole { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public int? GetUserId =>
            User is null ? null : (int?)int.Parse(User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value);        
        public UserRoleEnum? GetUserRole =>
            User is null ? null : (UserRoleEnum?)int.Parse(User.FindFirst(claim => claim.Type == ClaimTypes.Role).Value);
    }
}
