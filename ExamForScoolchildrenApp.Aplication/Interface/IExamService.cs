using ExamForScoolchildrenApp.Domain.Entities;

namespace ExamForScoolchildrenApp.Aplication.Interface
{
    public interface IExamService
    {
        Task<IEnumerable<Exam>> GetExamsAsync();
        Task<Exam> GetExamByIdAsync(int id);

        Task AddExamAsync(Exam exam);
        Task UpdateExamAsync(Exam exam);

        Task DeleteExamAsync(int id);
    }
}
