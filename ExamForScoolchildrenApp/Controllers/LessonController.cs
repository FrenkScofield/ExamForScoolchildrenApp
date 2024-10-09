
using ExamForScoolchildrenApp.Aplication.Services;
using ExamForScoolchildrenApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExamForScoolchildrenApp.Controllers
{
    [Route("Lesson")]
    public class LessonController : Controller
    {
        private readonly LessonService _lessonService;

        public LessonController(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var products = await _lessonService.GetLessonsAsync();

                return View(products);
            }
            else
            {
                return RedirectToAction("Signin", "Account");
            }
        }

        //Create Lesson
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("lesson/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null) return NotFound();
            return View(lesson);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Lesson lesson)
        {

            await _lessonService.AddLessonAsync(lesson);
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
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null) return NotFound();
            return View(lesson);
        }

        [HttpPost("update/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Lesson lesson)
        {
            if (id != lesson.Id) return BadRequest();
            await _lessonService.UpdateLessonAsync(lesson);
            return RedirectToAction(nameof(Index));
        }
        //Delete Lesson
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _lessonService.DeleteLessonAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
