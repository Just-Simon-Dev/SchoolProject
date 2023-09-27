using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.IRepositories;

public interface IEvidentionRepository : IRepository<EvidentionRecord>
{
    Task<EvidentionRecord?> GetFromEntity(EvidentionRecord evidentionRecord);
    Task<List<EvidentionRecord>> Filter(string? name, string? surname, string? classs, int? numberOfLessons, string? issues, string? lessonsTimes);
}