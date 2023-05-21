using Microsoft.AspNetCore.Authorization;

namespace SystemSprawozdan.Backend.Authorization
{
    public enum TeacherResourceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }
    public class TeacherResourceOperationRequirement : IAuthorizationRequirement
    {
        public TeacherResourceOperation TeacherResourceOperation { get; }
        public TeacherResourceOperationRequirement(TeacherResourceOperation teacherResourceOperation)
        {
            TeacherResourceOperation = teacherResourceOperation;
        }
    }
}
