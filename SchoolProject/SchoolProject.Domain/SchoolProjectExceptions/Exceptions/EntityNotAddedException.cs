namespace SchoolProject.Domain.SchoolProjectExceptions.Exceptions;

public class EntityNotAddedException : SchoolProjectException
{
    public static string message = "Error occured while adding new entity";
    public EntityNotAddedException() : base(message)
    {
    }

    public override string ErrorCode => "entity_not_added";
}