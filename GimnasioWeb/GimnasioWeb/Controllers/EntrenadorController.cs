using Microsoft.AspNetCore.Mvc;
using GimnasioWeb.Models;
using static System.Net.WebRequestMethods;
using GimnasioWeb.Servicios;

namespace GimnasioWeb.Controllers
{

    public class EntrenadorController : Controller
    {
        private readonly IMetodosComunes _comunes;
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _conf;

        public EntrenadorController(IHttpClientFactory http, IConfiguration conf, IMetodosComunes comunes)
        {
            _http = http;
            _conf = conf;
            _comunes = comunes;
        }

        [HttpGet]
        public IActionResult CrearEntrenador()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearEntrenador(Entrenador model)
        {
            using (var client = _http.CreateClient())
            {
                var url = _conf.GetSection("Variables:UrlApi").Value + "Entrenador/CrearEntrenador";
                model.Contrasena = _comunes.Encrypt(model.Contrasena);
                JsonContent datos = JsonContent.Create(model);

                var response = client.PostAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {

                    return RedirectToAction("Inicio", "Home");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View();
                }



            }
        }

        [HttpGet]
        public IActionResult crearClase()
        {
            return View();
        }

        [HttpPost]
        public IActionResult crearClase(Clase model)
        {
            using (var client = _http.CreateClient())
            {
                var url = _conf.GetSection("Variables:UrlApi").Value + "Entrenador/crearClase";
              
                JsonContent datos = JsonContent.Create(model);

                var response = client.PostAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {

                    return RedirectToAction("Inicio", "Home");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View();
                }



            }
        }
    }
    }

