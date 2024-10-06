using EduHomeFtoB.Contexts;
using EduHomeFtoB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeFtoB.Areas.AdminPanel.Controllers
{
    public class CourseController : AdminController
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Include(x => x.Category).ToListAsync();
            return View(courses);
        }
        public IActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create()
        //{
        //}
    }
}
