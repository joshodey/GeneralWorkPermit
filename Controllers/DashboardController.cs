using Microsoft.AspNetCore.Mvc;

namespace GeneralWorkPermit.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



    }
}
