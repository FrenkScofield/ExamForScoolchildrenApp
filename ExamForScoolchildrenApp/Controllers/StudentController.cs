using ExamForScoolchildrenApp.Aplication.Interface;
using ExamForScoolchildrenApp.Aplication.Services;
using ExamForScoolchildrenApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExamForScoolchildrenApp.Controllers
{
    [Route("Student")]
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;


        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _studentService.GetStudentsAsync();
            
            return View(products);

        }

        //Create Student
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();

        }

       [Route("student/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
         
            return View(student);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Student  student)
        {

            await _studentService.AddStudentAsync(student);
             return RedirectToAction(nameof(Index)); ;
        }
        //Update Student
        [HttpGet("update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var students = await _studentService.GetStudentByIdAsync(id);
            if (students == null) return NotFound();
            return View(students);
        }


        [HttpPost("update/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Student student)
        {
            if (id != student.Id) return BadRequest();
            await _studentService.UpdateStudentAsync(student);
            return RedirectToAction(nameof(Index));
        }

        //Delete Lesson
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
