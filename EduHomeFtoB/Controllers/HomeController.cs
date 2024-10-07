using EduHomeFtoB.Contexts;
using EduHomeFtoB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EduHomeFtoB.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var course = await _context.Courses.Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id==id);
            if (course == null) return NotFound();
            return View(course);
        }

    }
}
