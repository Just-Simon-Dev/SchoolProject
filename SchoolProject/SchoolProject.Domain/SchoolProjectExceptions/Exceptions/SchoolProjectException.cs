namespace SchoolProject.Domain.SchoolProjectExceptions.Exceptions
{
    public abstract class SchoolProjectException : Exception
    {
        public abstract string ErrorCode { get;  }
        protected SchoolProjectException(string message) : base(message) { }
    }
}
