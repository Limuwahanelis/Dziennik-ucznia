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
        public SchoolDbContext_SQLServer(DbContextOptions<SchoolDBContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-Dziennik_ucznia;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
                base.OnConfiguring(optionsBuilder);
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Student>()
            //    .HasMany(e => e.Grades)
            //    .WithOne(e => e.Student)
            //    .HasForeignKey(e => e.Id)
            //    .IsRequired();
        }
        public DbSet<DziennikUcznia.Models.Subject> Subject { get; set; } = default!;
    }
}
