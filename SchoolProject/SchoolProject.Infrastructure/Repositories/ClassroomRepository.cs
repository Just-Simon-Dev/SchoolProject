using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.IRepositories;
using SchoolProject.Domain.SchoolProjectExceptions.Exceptions;
using SchoolProject.Infrastructure.Context;

namespace SchoolProject.Infrastructure.Repositories;

public class ClassroomRepository : IClassroomRepository
{
    private readonly SchoolProjectContext _context;

    public ClassroomRepository(SchoolProjectContext context)
    {
        _context = context;
    }
    
    public async Task<Classroom> GetById(object id)
    {
        return await _context.Classrooms.FindAsync(id);
    }
    
    public async Task<List<Classroom>> GetAll()
    {
        return await _context.Classrooms.ToListAsync();
    }

    public async Task Add(Classroom entity)
    {
        var result = await _context.Classrooms.AddAsync(entity);
        if (result.State != EntityState.Added)
        {
            throw new EntityNotAddedException();
        }
    }
}