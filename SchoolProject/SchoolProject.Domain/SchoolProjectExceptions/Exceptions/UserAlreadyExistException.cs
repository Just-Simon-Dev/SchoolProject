namespace SchoolProject.Domain.SchoolProjectExceptions.Exceptions;

public class UserAlreadyExistException : SchoolProjectException
{
    public static string message = "User is already exist!";
    public UserAlreadyExistException() : base(message)
    {
    }

    public override string ErrorCode => "user_already_exists";
}