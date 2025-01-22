using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Data
{
    public class SchoolDbContext_MySQL: IdentityDbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }

        public SchoolDbContext_MySQL(DbContextOptions<SchoolDbContext_MySQL> options) : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=SchoolDb;Uid=root;Pwd=Password;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>()
                .HasMany(e => e.Grades)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.Id)
                .IsRequired();
        }
    }


}
