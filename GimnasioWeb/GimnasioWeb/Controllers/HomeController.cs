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

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Clases()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Blog()
        {
            return View();
        }
    }
}
