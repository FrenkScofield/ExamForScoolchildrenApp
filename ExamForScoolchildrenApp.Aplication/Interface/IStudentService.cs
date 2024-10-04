using ExamForScoolchildrenApp.Domain.Entities;

namespace ExamForScoolchildrenApp.Aplication.Interface
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);

        Task UpdateStudentAsync(Student student);

        Task DeleteStudentAsync(int id);

    }
}
