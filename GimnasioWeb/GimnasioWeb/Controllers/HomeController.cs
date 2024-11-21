using System.Diagnostics;
using GimnasioWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace GimnasioWeb.Controllers
{
    public class HomeController : Controller
    {
   

        public IActionResult Inicio()
        {
            return View();
        }

    
    }
}
