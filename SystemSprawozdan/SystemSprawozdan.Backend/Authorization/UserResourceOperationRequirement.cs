using Microsoft.AspNetCore.Authorization;

namespace SystemSprawozdan.Backend.Authorization
{
    public enum UserResourceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }
    public class UserResourceOperationRequirement : IAuthorizationRequirement
    {
        public UserResourceOperation UserResourceOperation { get; }
        public UserResourceOperationRequirement(UserResourceOperation userResourceOperation)
        {
            UserResourceOperation = userResourceOperation;
        }
    }
}
