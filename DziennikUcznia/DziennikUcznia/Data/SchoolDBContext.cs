using DziennikUcznia.Identity;
using DziennikUcznia.Interfaces;
using DziennikUcznia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Data
{
    public abstract class SchoolDBContext : IdentityDbContext<AppUser>, IDbContext
    {
        public SchoolDBContext() : base() { }
        public SchoolDBContext(DbContextOptions<SchoolDBContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SchoolClass> Classes { get; set; }

        public DbSet<Subject> Subjects { get; set; }
    }
}
