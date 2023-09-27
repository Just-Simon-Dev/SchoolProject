using Microsoft.AspNetCore.Http;
using SchoolProject.Domain.SchoolProjectExceptions.Exceptions;

namespace SchoolProject.Application;

public interface IUserContext
{
    int GetUserIdOrThrow { get; }
    int? GetUserId { get; }
    bool IsAuthenticated { get; }
}

public class UserContext : IUserContext
{
    private readonly HttpContext? _httpContext;

    public UserContext(IHttpContextAccessor contextAccessor)
    {
        _httpContext = contextAccessor.HttpContext;
    }

    public int GetUserIdOrThrow => GetUserId ?? throw new NoAccessException();

    public int? GetUserId
    {
        get
        {
            var userIdString = _httpContext?.User.FindFirst(c => c.Type == "Id")?.Value;

            int.TryParse(userIdString, out var userId);

            if (userId == default)
            {
                return null;
            }

            return userId;
        }
    }

    public bool IsAuthenticated => GetUserId.HasValue;
}