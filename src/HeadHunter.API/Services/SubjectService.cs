using System.Linq;
using System.Threading.Tasks;
using HeadHunter.API.Models;
using HeadHunter.API.Repositories;

namespace HeadHunter.API.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _repository;

        public SubjectService(ISubjectRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<Subject> GetAll() => _repository.GetAll();

        public async Task<Subject> GetByEmail(string email)
        {
            return await _repository.GetByEmail(email);
        }

        public async Task<Subject> GetById(int id)
        {
            return await _repository.GetById(id);
        }
        public async Task<Subject> Create(Subject entity)
        {
            return await _repository.Create(entity);
        }

        public async Task<Subject> Update(int id, Subject entity)
        {
            return await _repository.Update(id, entity);
        }

        public async Task<Subject> Delete(int id)
        {
            return await _repository.Delete(id);

        }

    }
}
