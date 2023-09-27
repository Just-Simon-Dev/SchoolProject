using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Infrastructure.Context
{
    public class SchoolProjectContext : DbContext
    {
        public SchoolProjectContext(DbContextOptions<SchoolProjectContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<EvidentionRecord> EvidentionRecords { get; set; }
    }
}
