using System.Linq;
using System.Threading.Tasks;
using HeadHunter.API.Models;

namespace HeadHunter.API.Repositories
{
    public interface ISubjectRepository
    {
        Task<Subject> Create(Subject entity);
        Task<Subject> Delete(int id);
        IQueryable<Subject> GetAll();
        Task<Subject> GetByEmail(string email);
        Task<Subject> GetById(int id);
        Task<Subject> Update(int id, Subject entity);
    }
}