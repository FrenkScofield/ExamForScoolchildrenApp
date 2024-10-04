using ExamForScoolchildrenApp.Aplication.Interface;
using ExamForScoolchildrenApp.Application.Interface;
using ExamForScoolchildrenApp.Domain.Entities;
using ExamForScoolchildrenApp.Domain.Interfaces;

namespace ExamForScoolchildrenApp.Aplication.Services
{
    public class StudentService:IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _unitOfWork.Students.GetAllAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _unitOfWork.Students.GetByIdAsync(id);
        }

        public async Task AddStudentAsync(Student student )
        {
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _unitOfWork.Students.Update(student);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student != null)
            {
                _unitOfWork.Students.Delete(student);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
