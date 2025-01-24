using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;
using DziennikUcznia.Interfaces;

namespace DziennikUcznia.Data
{
    public class SchoolDbContext_MySQL : SchoolDBContext
    {
        public SchoolDbContext_MySQL(DbContextOptions<SchoolDbContext_MySQL> options) : base()
        {

        }
        public SchoolDbContext_MySQL(DbContextOptions<SchoolDBContext> options) : base(options)
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
