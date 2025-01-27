using DziennikUcznia.Data;
using DziennikUcznia.Models;
using DziennikUcznia.Repositories;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace DziennikUcznia.UnitTest
{
    public class ClassesRepositoryTests:IDisposable
    {
        string connectionString;
        SchoolDbContext_SQLServer context;
        public ClassesRepositoryTests() 
        {
            connectionString = $"Server=(localdb)\\mssqllocaldb;Database=DziennikTestDB{Guid.NewGuid()};Trusted_Connection=True;";
            context = new SchoolDbContext_SQLServer(new DbContextOptionsBuilder<SchoolDBContext>()
            .UseSqlServer(connectionString).Options);
            context.Database.EnsureCreated();
        }
        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }
        [Fact]
        public async void GetClassesList_ReturnsListOf4Length()
        {

            List<string> classNames = new List<string>() { "2A", "3C", "1B", "2F" };
            ClassesRepository repository = new ClassesRepository(context);
            foreach (string className in classNames)
            {
                await repository.AddClass(className);
            }
            List<SchoolClass> classes = await repository.GetClasses();
            Assert.Equal(4, classes.Count);
        }
        [Fact]
        public async void GetClassById_ReturnsClassWithName1B()
        {
            
            List<string> classNames = new List<string>() { "2A", "3C", "1B", "2F" };
            ClassesRepository repository = new ClassesRepository(context);
            foreach (string className in classNames)
            {
                await repository.AddClass(className);
            }
            SchoolClass? tclass = await repository.GetClassById(3);
            Assert.Equal("1B", tclass.Name);
        }


    }

}