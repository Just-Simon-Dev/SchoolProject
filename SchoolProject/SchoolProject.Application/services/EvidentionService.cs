using SchoolProject.Application.Commands;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Enums;
using SchoolProject.Domain.IRepositories;
using SchoolProject.Domain.SchoolProjectExceptions.Exceptions;

namespace SchoolProject.Application.services;

public interface IEvidentionService
{
    Task AddRecord(string ipAdress, AddEvidentionRecordCommand command);
    Task<List<EvidentionRecord>> Filter(FilterEvidentionCommand command);
}

public class EvidentionService : IEvidentionService
{
    private readonly IUserRepository _userRepository;
    private readonly IEvidentionRepository _evidentionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthenticationService _authenticationService;

    public EvidentionService(IUserRepository userRepository, IAuthenticationService authenticationService, IEvidentionRepository evidentionRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _evidentionRepository = evidentionRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task AddRecord(string ipAdress, AddEvidentionRecordCommand command)
    {
        // try to get student account
        var username = $"{command.Name} {command.Surname}";
        var student = await _userRepository.GetByNameAsync(username);
        
        // if not create new student account
        if (student == null)
        {
            var registrationCommand = new RegisterationCommand(username, "student", Roles.Student, command.Class);
            // TODO check if user id is null here!
            student = await _authenticationService.Register(registrationCommand);
        }

        // create EvidentionRecord entity
        var evidentionRecord =
            new EvidentionRecord(ipAdress, command.NumberOfLessons, command.LessonsTimes, command.Issues);
        evidentionRecord.SetStudent(student);

        if (evidentionRecord.Issues == null)
        {
            evidentionRecord.Issues = "";
        }
        
        // check if user didn't add record before
        var evidention = await _evidentionRepository.GetFromEntity(evidentionRecord);
        
        if (evidention != null)
        {
            throw new NoAccessException();
        }
        
        // add new record to database
        await _evidentionRepository.Add(evidentionRecord);
        // commit
        await _unitOfWork.commit();
    }

    public async Task<List<EvidentionRecord>> Filter(FilterEvidentionCommand command)
    {
        var filteredEvidentionRecords = await _evidentionRepository.Filter(command.Name, command.Surname, command.Class, command.NumberOfLessons, command.Issues, command.LessonsTimes);
        return filteredEvidentionRecords;
    }
}