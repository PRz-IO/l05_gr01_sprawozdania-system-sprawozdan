namespace SystemSprawozdan.Backend.Exceptions
{
    public class ForbidException : Exception
    {
        public ForbidException(string message = "Access is forbidden!") : base(message)
        {
        }
    }
}
