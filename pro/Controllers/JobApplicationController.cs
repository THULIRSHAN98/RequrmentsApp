using Microsoft.AspNetCore.Mvc;

namespace pro.Controllers
{
    public class JobApplicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
