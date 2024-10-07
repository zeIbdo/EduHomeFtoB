using EduHomeFtoB.Contexts;
using EduHomeFtoB.Models;
using EduHomeFtoB.Models.CourseViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeFtoB.Areas.AdminPanel.Controllers
{

    public class CourseController : AdminController
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public CourseController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Include(x => x.Category).ToListAsync();
            return View(courses);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateViewModel vm)
        {

            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;


            if (!ModelState.IsValid)
            {
                return View(vm);

            }

            var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);

            if (!isExistCategory)
            {
                ModelState.AddModelError("CategoryId", "Please select correct variant");
                return View(vm);
            }


            if (vm.StartDate < DateTime.UtcNow)
            {
                ModelState.AddModelError("StartDate", "Kecmis tarix sece bilmezsiniz");
                return View(vm);
            }


            if (!vm.CourseImage.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Please enter valid input");
                return View(vm);
            }
            if (vm.CourseImage.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Max image size-2mb");
                return View(vm);
            }



            string filename = Guid.NewGuid() + vm.CourseImage.FileName.Substring(vm.CourseImage.FileName.LastIndexOf('.'));

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", filename);

            using (FileStream stream = new(path, FileMode.CreateNew))
            {
                await vm.CourseImage.CopyToAsync(stream);
            }




            Course course = new()
            {
                Description = vm.Description,
                CategoryId = vm.CategoryId,
                Duration = vm.Duration,
                Name = vm.Name,
                Fee = vm.Fee,
                StartDate = vm.StartDate,
                ImageUrl = filename
            };

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);

            if (course is null)
                return NotFound();

            CourseUpdateViewModel vm = new()
            {
                CategoryId = course.CategoryId,
                Id = course.Id,
                Description = course.Description,
                Duration = course.Duration,
                StartDate = course.StartDate,
                Fee = course.Fee,
                Name = course.Name
            };

            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateViewModel vm)
        {
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;


            if (!ModelState.IsValid)
            {
                return View(vm);

            }
            var existingCourse = await _context.Courses.FindAsync(vm.Id);
            if (existingCourse == null) return BadRequest();
            if (vm.CourseImage != null)
            {

                if (!vm.CourseImage.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("Image", "Please enter valid input");
                    return View(vm);
                }
                if (vm.CourseImage.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("Image", "Max image size-2mb");
                    return View(vm);
                }
                var newName = Guid.NewGuid() + vm.CourseImage.FileName.Substring(vm.CourseImage.FileName.LastIndexOf('.'));
                string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", newName);
                using (FileStream fs = new(newPath, FileMode.Create))
                {
                    await vm.CourseImage.CopyToAsync(fs);
                }
                var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", existingCourse.ImageUrl);
                if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                existingCourse.ImageUrl = newName;
            }
            var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);

            if (!isExistCategory)
            {
                ModelState.AddModelError("CategoryId", "Please select correct variant");
                return View(vm);
            }
            if (vm.StartDate < DateTime.UtcNow)
            {
                ModelState.AddModelError("StartDate", "Kecmis tarix sece bilmezsiniz");
                return View(vm);
            }
            existingCourse.StartDate = vm.StartDate;
            existingCourse.Duration = vm.Duration;
            existingCourse.Description = vm.Description;
            existingCourse.CategoryId = vm.CategoryId;
            existingCourse.Fee = vm.Fee;
            existingCourse.Name = vm.Name;
            _context.Update(existingCourse);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null) return NotFound();
            var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", course.ImageUrl);
            if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
    }
}