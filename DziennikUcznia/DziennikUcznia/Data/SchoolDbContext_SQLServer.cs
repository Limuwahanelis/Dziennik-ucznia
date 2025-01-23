using DziennikUcznia.Identity;
using DziennikUcznia.Interfaces;
using DziennikUcznia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Data
{
    public class SchoolDbContext_SQLServer:SchoolDBContext
    {
        public SchoolDbContext_SQLServer(DbContextOptions<SchoolDbContext_SQLServer> options) : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-Dziennik_ucznia;Trusted_Connection=True;MultipleActiveResultSets=true");
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
