using ExamForScoolchildrenApp.Aplication.Interface;
using ExamForScoolchildrenApp.Application.Interface;
using ExamForScoolchildrenApp.Domain.Entities;
using ExamForScoolchildrenApp.Domain.Interfaces;

namespace ExamForScoolchildrenApp.Aplication.Services
{
    public class ExamService:IExamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Exam>> GetExamsAsync()
        {
            return await _unitOfWork.Exams.GetAllAsync();
        }

        public async Task<Exam> GetExamByIdAsync(int id)
        {
            return await _unitOfWork.Exams.GetByIdAsync(id);
        }

        public async Task AddExamAsync(Exam exam)
        {
            await _unitOfWork.Exams.AddAsync(exam);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateExamAsync(Exam exam)
        {
            _unitOfWork.Exams.Update(exam);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteExamAsync(int id)
        {
            var exam = await _unitOfWork.Exams.GetByIdAsync(id);
            if (exam != null)
            {
                _unitOfWork.Exams.Delete(exam);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
