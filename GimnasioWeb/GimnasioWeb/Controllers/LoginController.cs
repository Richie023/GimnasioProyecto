using GimnasioWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace GimnasioWeb.Controllers
{
    public class LoginController : Controller

    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _conf;

        public LoginController(IHttpClientFactory httpClient, IConfiguration conf)
        {
            _http = httpClient;
            _conf = conf;
        }

        [HttpGet]
        public ActionResult InicioSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InicioSesion(Usuario model)
        {
            return View();

        }


        [HttpGet]
        public IActionResult CrearCuenta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearCuenta(Usuario model)
        {
            using (var client = _http.CreateClient())
            {
                var url = _conf.GetSection("Variables:UrlApi").Value + "Login/CrearCuenta";
             JsonContent datos = JsonContent.Create(model);

                var response = client.PostAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if(result != null && result.Codigo == 0)
                {
                    return RedirectToAction("InicioSesion", "Login");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View();
                }


           
            }
        }


        [HttpGet]
        public IActionResult RecuperarAcceso()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarAcceso(Usuario model)
        {
            return View();
        }

    }
}