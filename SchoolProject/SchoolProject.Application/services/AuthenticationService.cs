using SchoolProject.Application.Commands;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Enums;
using SchoolProject.Domain.IRepositories;
using SchoolProject.Domain.SchoolProjectExceptions.Exceptions;

namespace SchoolProject.Application.services;

public interface IAuthenticationService
{
    Task<string> Login(LoginCommand loginCommand);
    Task<User> Register(RegisterationCommand registerationCommand);
    Task<List<Classroom>> GetClassrooms();
    Task SetClassroomsToUser(SetClassroomToUserCommand command);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly IHashService _hashService;
    private readonly IJwtService _jwtService;
    private readonly IUserContext _userContext;

    public AuthenticationService(IUnitOfWork unitOfWork, IUserRepository userRepository, IHashService hashService, IClassroomRepository classroomRepository, IJwtService jwtService, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _hashService = hashService;
        _classroomRepository = classroomRepository;
        _jwtService = jwtService;
        _userContext = userContext;
    }
    public async Task<string> Login(LoginCommand loginCommand)
    {
        var user = await _userRepository.GetByNameAsync(loginCommand.Login);
        
        if (user == null || user.Role == Roles.Student)
        {
            throw new UserNotFoundException();
        }
        
        var isPasswordCorrect = _hashService.VerifyPassword(loginCommand.Password, user.Password);
        
        if (!isPasswordCorrect)
        {
            throw new UserNotFoundException();
        }
        
        var token = await _jwtService.GenerateToken(user);

        return token;
    }

    public async Task<User> Register(RegisterationCommand registerationCommand)
    {
        // check the role of the adding user
        var adminUserId = _userContext.GetUserId;
        if (adminUserId != null)
        {
            var adminUser = await _userRepository.GetById(adminUserId);
            if (adminUser.Role != Roles.Admin)
            {
                throw new NoAccessException();
            }
        }
        
        // check if user is not already exist
        var user = await _userRepository.GetByNameAsync(registerationCommand.Login);
        if (user != null)
        {
            throw new UserAlreadyExistException();
        }
        // create new user with hash password
        var hashedPassword = await _hashService.HashPassword(registerationCommand.Password);
        
        var newUser = new User(registerationCommand.Login, hashedPassword);
        newUser.SetRole(registerationCommand.Role);
        newUser.Class = registerationCommand.Class;
        
        // add user to database
        await _userRepository.Add(newUser);
        // commit changes into database
        await _unitOfWork.commit();
        // return new user
        return newUser;
    }

    public async Task<List<Classroom>> GetClassrooms()
    {
        var classrooms = await _classroomRepository.GetAll();
        return classrooms;
    }

    public async Task SetClassroomsToUser(SetClassroomToUserCommand command)
    {
        var userId = _userContext.GetUserIdOrThrow;
        var user = await _userRepository.GetById(userId);
        
        var classroom = await _classroomRepository.GetById(command.Id);
        user.SetClassroom(classroom);
        
        await _unitOfWork.commit();
    }
}