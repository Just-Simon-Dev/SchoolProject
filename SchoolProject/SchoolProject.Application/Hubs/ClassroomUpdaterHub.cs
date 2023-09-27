using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.IRepositories;
using SignalRSwaggerGen.Attributes;

namespace SchoolProject.Application.Hubs;

public interface IClassroomUpdaterHub
{
    Task AddEvidention(EvidentionRecord evidentionRecord);
    Task UpdateTeacherBoard(string classroom);
}

[SignalRHub]
public class ClassroomUpdaterHub : Hub
{
    private readonly IUserRepository _userRepository;
    private static readonly IDictionary<string, List<EvidentionRecord>> AddedEvidention = new Dictionary<string, List<EvidentionRecord>>();
    
    private static readonly IDictionary<string, string> TeachersForClass = new Dictionary<string, string>();

    public ClassroomUpdaterHub(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task AddEvidention(EvidentionRecord evidentionRecord)
    {
        var groupId = GetClassname(evidentionRecord.Student.Classroom.Name,evidentionRecord.Student.Class);
        if (AddedEvidention[groupId] == null)
        {
            AddedEvidention[groupId] = new List<EvidentionRecord>();
        }
        AddedEvidention[groupId].Add(evidentionRecord);
        
        await UpdateTeacherBoard(evidentionRecord.Student.Classroom.Name);
    }

    public Task UpdateTeacherBoard(string classroom)
    {
        var teacher = _userRepository.GetByClassAsync(classroom).Result;
        var groupId = GetClassname(teacher.Classroom.Name, teacher.Class);

        if (TeachersForClass[classroom] == null)
        {
            TeachersForClass[classroom] = teacher.Login;
        }

        if (teacher.Login != TeachersForClass[classroom])
        {
            AddedEvidention[groupId] = new List<EvidentionRecord>();
        }
        
        var evidentionRecords = AddedEvidention[groupId];
        
        return Clients.Group(groupId).SendAsync("SendMessage", "sender", evidentionRecords);
    }

    public static string GetClassname(string? classroom, string classname) => $"class_{classroom}_{classname}";
}