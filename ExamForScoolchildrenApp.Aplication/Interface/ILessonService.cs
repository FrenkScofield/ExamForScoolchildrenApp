using ExamForScoolchildrenApp.Domain.Entities;
using System.Threading.Tasks;

namespace ExamForScoolchildrenApp.Aplication.Interface
{
    public interface ILessonService
    {
        Task<IEnumerable<Lesson>> GetLessonsAsync();

        Task<Lesson> GetLessonByIdAsync(int id);

        Task AddLessonAsync(Lesson lesson);

        Task UpdateLessonAsync(Lesson lesson);

        Task DeleteLessonAsync(int id);

    }
}
