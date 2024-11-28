using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Welcome to the Library Management System! The application is running.");
        }
    }
}
