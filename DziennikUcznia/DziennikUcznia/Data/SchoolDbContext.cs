using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Data
{
    public class SchoolDbContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=SchoolDb;Uid=root;Pwd=Password;");
            base.OnConfiguring(optionsBuilder);
        }
    }

    
}
