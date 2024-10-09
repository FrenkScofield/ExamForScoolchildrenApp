using ExamForScoolchildrenApp.Aplication.Services;
using ExamForScoolchildrenApp.Domain.Entities;
using ExamForScoolchildrenApp.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ExamForScoolchildrenApp.Controllers
{
  
    [Route("Exam")]
    public class ExamController : Controller
    {
        private readonly ExamService _examService;
        private readonly LessonService _lessonService;
        private readonly StudentService _studentService;



        public ExamController(ExamService examService, LessonService lessonService, StudentService studentService)
        {
            _examService = examService;
            _lessonService = lessonService;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            { 
                var exams = await _examService.GetExamsAsync();
            
            return View(exams);
            }
            else
            {
                return RedirectToAction("Signin", "Account");
            }

        }
        //Create Exam
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var viewModel = new ExamViewModel
            {
                Exam = new Exam(),
                Students = await _studentService.GetStudentsAsync(),
                Lessons = await _lessonService.GetLessonsAsync()

            };
            return View(viewModel);

        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ExamViewModel viewModel)
        {
            if (viewModel.Exam.LessonId == 0 && viewModel.Exam.StudentId == 0)
            {
                return RedirectToAction(nameof(Create));
            }
            await _examService.AddExamAsync(viewModel.Exam);
            return RedirectToAction(nameof(Index));
        }

        //  Update Lesson
        [HttpGet("update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _examService.GetExamByIdAsync(id);

            ExamViewModel viewModel = new ExamViewModel();
            {
                viewModel.Exam = exam;
                viewModel.Students = await _studentService.GetStudentsAsync();
                viewModel.Lessons = await _lessonService.GetLessonsAsync();

            }
            if (viewModel == null) return NotFound();
            return View(viewModel);
        }

        [HttpPost("update/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, ExamViewModel viewModel)
        {
            var examEntity = await _examService.GetExamByIdAsync(id);

            if (examEntity == null)
            {
                return NotFound();
            }

            examEntity.LessonId = viewModel.Exam.LessonId;
            examEntity.StudentId = viewModel.Exam.StudentId;
            examEntity.ImtahanTarixi = viewModel.Exam.ImtahanTarixi;
            examEntity.Qiymeti = viewModel.Exam.Qiymeti;

            await _examService.UpdateExamAsync(examEntity);
            return RedirectToAction(nameof(Index));
        }

        //Delete Lesson
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _examService.DeleteExamAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
