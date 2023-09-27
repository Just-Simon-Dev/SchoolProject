namespace SchoolProject.Domain.SchoolProjectExceptions.Exceptions;

public class UserNotFoundException : SchoolProjectException
{
    public static string message = "User not found";

    public UserNotFoundException() : base(message)
    {
    }

    public override string ErrorCode => "user_not_found";
}