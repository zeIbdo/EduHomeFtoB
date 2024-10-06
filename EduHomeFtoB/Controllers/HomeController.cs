using EduHomeFtoB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EduHomeFtoB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

      
    }
}
