using System.ComponentModel.DataAnnotations;

namespace ExamForScoolchildrenApp.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string StudentName { get; set; }

        [StringLength(30)]
        public string StudentSurname { get; set; }

        public int StudentClassNum { get; set; }

        public ICollection<Exam> Exams { get; set; }
    }
}
