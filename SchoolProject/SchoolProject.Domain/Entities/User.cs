using SchoolProject.Domain.Enums;

namespace SchoolProject.Domain.Entities
{
    public class User
    {
        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Class { get; set; }
        public Classroom? Classroom { get; set; } = null;
        public Roles Role { get; set; }

        public void SetClassroom(Classroom classroom)
        {
            Classroom = classroom;
        }

        public void SetRole(Roles role)
        {
            Role = role;
        }
    }
}
