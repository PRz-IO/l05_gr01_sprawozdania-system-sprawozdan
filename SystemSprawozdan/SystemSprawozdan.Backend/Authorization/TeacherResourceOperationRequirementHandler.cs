using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SystemSprawozdan.Shared.Enums;

namespace SystemSprawozdan.Backend.Authorization
{
    public class TeacherResourceOperationRequirementHandler : AuthorizationHandler<TeacherResourceOperationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TeacherResourceOperationRequirement requirement)
        {
            if (!int.TryParse(context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value, out int role))
            {
                return Task.CompletedTask;
            }

            if (role == (int)UserRoleEnum.Teacher)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
