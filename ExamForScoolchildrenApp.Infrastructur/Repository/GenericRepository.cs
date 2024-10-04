using ExamForScoolchildrenApp.Domain.Interfaces;
using ExamForScoolchildrenApp.Infrastructur.Data;
using Microsoft.EntityFrameworkCore;

namespace ExamForScoolchildrenApp.Infrastructur.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ExamForScoolDBContext _context;

        public GenericRepository(ExamForScoolDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
