using SchoolProject.Domain.Enums;

namespace SchoolProject.Application.Commands;

public class RegisterationCommand
{
    public string Login { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; } = Roles.Teacher;
    public string? Class { get; set; } = null;

    public RegisterationCommand(string login, string password, Roles role, string classs)
    {
        Login = login;
        Password = password;
        Role = role;
        Class = classs;
    }
    
}