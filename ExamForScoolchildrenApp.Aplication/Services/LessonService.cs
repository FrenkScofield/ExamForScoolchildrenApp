using ExamForScoolchildrenApp.Aplication.Interface;
using ExamForScoolchildrenApp.Application.Interface;
using ExamForScoolchildrenApp.Domain.Entities;
using ExamForScoolchildrenApp.Domain.Interfaces;

namespace ExamForScoolchildrenApp.Aplication.Services
{
    public class LessonService:ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LessonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Lesson>> GetLessonsAsync()
        {
            return await _unitOfWork.Lessons.GetAllAsync();
        }

        public async Task<Lesson> GetLessonByIdAsync(int id)
        {
            return await _unitOfWork.Lessons.GetByIdAsync(id);
        }

        public async Task AddLessonAsync(Lesson lesson)
        {
            await _unitOfWork.Lessons.AddAsync(lesson);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateLessonAsync(Lesson lesson)
        {
            _unitOfWork.Lessons.Update(lesson);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteLessonAsync(int id)
        {
            var Lesson = await _unitOfWork.Lessons.GetByIdAsync(id);
            if (Lesson != null)
            {
                _unitOfWork.Lessons.Delete(Lesson);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
