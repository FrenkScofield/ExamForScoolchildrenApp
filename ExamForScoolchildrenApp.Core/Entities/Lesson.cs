using System.ComponentModel.DataAnnotations;

namespace ExamForScoolchildrenApp.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string LessonName { get; set; }

        public int LessonClassNum { get; set; }

        [StringLength(20)]
        public string TeacherName { get; set; }

        [StringLength(20)]
        public string TeacherSurname { get; set; }

        public ICollection<Exam> Exams { get; set; }
    }
}
