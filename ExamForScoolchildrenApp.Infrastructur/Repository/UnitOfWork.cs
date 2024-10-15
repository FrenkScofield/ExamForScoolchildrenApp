using ExamForScoolchildrenApp.Application.Interface;
using ExamForScoolchildrenApp.Domain.Entities;
using ExamForScoolchildrenApp.Domain.Interfaces;
using ExamForScoolchildrenApp.Infrastructur.Data;

namespace ExamForScoolchildrenApp.Infrastructur.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExamForScoolDBContext _context;

        private IGenericRepository<Student> _studentRepository;
        private IGenericRepository<Lesson> _lessonRepository;
        private IGenericRepository<Exam> _examRepository;

        public UnitOfWork(ExamForScoolDBContext context)
        {
            _context = context;
        }

        public IGenericRepository<Student> Students => _studentRepository ??= new GenericRepository<Student>(_context);
        public IGenericRepository<Lesson> Lessons => _lessonRepository ??= new GenericRepository<Lesson>(_context);
        public IGenericRepository<Exam> Exams => _examRepository ??= new GenericRepository<Exam>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
