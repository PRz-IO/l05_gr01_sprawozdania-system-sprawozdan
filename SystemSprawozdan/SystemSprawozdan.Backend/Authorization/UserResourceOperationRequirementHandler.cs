using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SystemSprawozdan.Backend.Data.Enums;
using SystemSprawozdan.Backend.Data.Models.Others;

namespace SystemSprawozdan.Backend.Authorization
{
    public class UserResourceOperationRequirementHandler : AuthorizationHandler<UserResourceOperationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserResourceOperationRequirement requirement)
        {
            if (!int.TryParse(context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value, out int role))
            {
                return Task.CompletedTask;
            }

            if (role == (int)UserRoleEnum.Admin)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
