
namespace ExamForScoolchildrenApp.Domain.Entities
{
    public class Exam
    {
        public int Id { get; set; }

        //  [ForeignKey("Lesson")]
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        // [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public DateTime ImtahanTarixi { get; set; }

        public int Qiymeti { get; set; }
    }
}
