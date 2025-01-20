using DziennikUcznia.Repositories;

namespace DziennikUcznia.Services
{
    public class TestService
    {
        private readonly SchoolRepository _repository;

        public TestService(SchoolRepository repository) 
        {
            _repository = repository;
        }

        public void Test()
        {
            _repository.TestDb();
        }

    }
}
