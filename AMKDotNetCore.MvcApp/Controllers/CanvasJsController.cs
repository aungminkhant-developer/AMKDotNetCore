using Microsoft.AspNetCore.Mvc;

namespace AMKDotNetCore.MvcApp.Controllers
{
    public class CanvasJsController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
    }
}
