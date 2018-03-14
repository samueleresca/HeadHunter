using System;
using System.Linq;
using System.Threading.Tasks;
using HeadHunter.API.Infrastructure;
using HeadHunter.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HeadHunter.API.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly FeedbackContext _dbContext;

        public SubjectRepository(FeedbackContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Subject> GetAll() =>  _dbContext.Set<Subject>().AsNoTracking();

        public async Task<Subject> GetByEmail(string email)
        {
  
            return await _dbContext.Set<Subject>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Subject> GetById(int id)
        {
            return await _dbContext.Set<Subject>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Subject> Create(Subject entity)
        {
            if (entity == null) throw new Exception("Entity cannot be null");

            var result = await _dbContext.Set<Subject>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Subject> Update(int id, Subject entity)
        {
            if (entity == null) throw new Exception("Entity cannot be null");

            var oldEntity = _dbContext.Subjects.FirstOrDefault(_ => _.Id == id);

            entity.Id = id;
            
            _dbContext.Entry(oldEntity ?? throw new Exception("Entity cannot be null")).State = EntityState.Detached;
            _dbContext.Subjects.Attach(entity);

            var result = _dbContext.Entry(entity);
            result.State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Subject> Delete(int id)
        {
            var entity = await _dbContext.FindAsync<Subject>(id);
            entity.IsDeleted = true;

            return await Update(id, entity);
        }


    }
}
