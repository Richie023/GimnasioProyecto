using Microsoft.AspNetCore.Mvc;

namespace GimnasioWeb.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult MostrarError()
        {
            return View("Error");
        }
    }
}
