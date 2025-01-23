using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Interfaces
{
    public interface IDbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
    }
}
