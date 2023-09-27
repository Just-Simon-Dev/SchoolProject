namespace SchoolProject.Domain.SchoolProjectExceptions.Exceptions;

public class NoAccessException : SchoolProjectException
{
    private static string message = "User had no access to perform this action.";
    public NoAccessException() : base(message)
    {
    }

    public override string ErrorCode => "no_access";
}