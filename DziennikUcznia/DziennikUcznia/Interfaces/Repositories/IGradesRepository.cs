using DziennikUcznia.Data;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Interfaces.Repositories
{
    public interface IGradesRepository
    {
        public Task AddGrade(Grade grade);
    }
}
