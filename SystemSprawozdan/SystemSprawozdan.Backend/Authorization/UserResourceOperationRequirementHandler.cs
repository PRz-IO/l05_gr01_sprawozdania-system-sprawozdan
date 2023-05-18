using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SystemSprawozdan.Shared.Enums;

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

            if (role == (int)UserRoleEnum.Student)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
