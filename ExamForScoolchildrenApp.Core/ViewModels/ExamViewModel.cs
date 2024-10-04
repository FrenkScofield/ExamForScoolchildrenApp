using ExamForScoolchildrenApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamForScoolchildrenApp.Domain.ViewModels
{
    public class ExamViewModel
    {
        public Exam Exam { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
      
    }
}
