using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DziennikUcznia.Data
{
    public class SchoolDbContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=SchoolDb;Uid=root;Pwd=Password;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Student>()
                    .HasMany(e => e.Grades)
                    .WithOne(e => e.Student)
                    .HasForeignKey(e => e.Id)
                    .IsRequired();
        }
    }

    
}
