using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.IRepositories;
using SchoolProject.Domain.SchoolProjectExceptions.Exceptions;
using SchoolProject.Infrastructure.Context;

namespace SchoolProject.Infrastructure.Repositories;

public class EvidentionRepository : IEvidentionRepository
{
    private readonly SchoolProjectContext _context;

    public EvidentionRepository(SchoolProjectContext context)
    {
        _context = context;
    }
    
    public async Task<EvidentionRecord> GetById(object id)
    {
        return await _context.EvidentionRecords.FindAsync(id);
    }
    
    public async Task<List<EvidentionRecord>> GetAll()
    {
        return await _context.EvidentionRecords.ToListAsync();
    }

    public async Task Add(EvidentionRecord entity)
    {
        var result = await _context.EvidentionRecords.AddAsync(entity);
        if (result.State != EntityState.Added)
        {
            throw new EntityNotAddedException();
        }
    }

    public async Task<EvidentionRecord?> GetFromEntity(EvidentionRecord evidentionRecord)
    {
        var result = await _context.EvidentionRecords.FirstOrDefaultAsync(x =>
            x.Student.Id == evidentionRecord.Student.Id && x.LessonsTimes == evidentionRecord.LessonsTimes &&
            x.LessonsTimes == evidentionRecord.LessonsTimes);
        
        return result;
    }

    public async Task<List<EvidentionRecord>> Filter(string? name, string? surname, string? classs, int? numberOfLessons, string? issues, string? lessonsTimes)
    {
        var result = _context.EvidentionRecords.AsQueryable();
        if (name != null && surname != null)
        {
            var login = $"{name} {surname}";
            result = result.Where(x => x.Student.Login == login);
        }

        if (classs != null)
        {
            result = result.Where(x => x.Student.Class == classs);
        }

        if (numberOfLessons != null)
        {
            result = result.Where(x => x.NumberOfLessons == numberOfLessons);
        }

        if (issues != null)
        {
            result = result.Where(x => x.Issues == issues);
        }

        if (lessonsTimes != null)
        {
            result = result.Where(x => x.LessonsTimes == lessonsTimes);
        }

        return await result.ToListAsync();
    }
}