using ExamForScoolchildrenApp.Domain.Entities;
using ExamForScoolchildrenApp.Domain.Interfaces;

namespace ExamForScoolchildrenApp.Application.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Student> Students { get; }
        IGenericRepository<Lesson> Lessons { get; }
        IGenericRepository<Exam> Exams { get; }
        Task<int> SaveChangesAsync();
    }
}
