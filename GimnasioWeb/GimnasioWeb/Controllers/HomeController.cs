using System;
using System.Diagnostics;
using System.Text.Json;
using GimnasioWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace GimnasioWeb.Controllers
{
    public class HomeController : Controller
    {

            private readonly IHttpClientFactory _http;
            private readonly IConfiguration _conf;
            public HomeController(IHttpClientFactory http, IConfiguration conf)
            {
                _http = http;
                _conf = conf;
            }

        [HttpGet]
        public IActionResult Inicio()
        {
            return View();
        }

        [HttpGet]
            public IActionResult Productos()
            {
                using (var client = _http.CreateClient())
                {
                    string url = _conf.GetSection("Variables:UrlApi").Value + "Producto/ConsultarProductos";

                    var response = client.GetAsync(url).Result;
                    var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                    if (result != null && result.Codigo == 0)
                    {
                        var datosContenido = JsonSerializer.Deserialize<List<Producto>>((JsonElement)result.Contenido!);
                        return View(datosContenido!.Where(x => x.Inventario > 0 && x.Estado == "Activo").ToList());
                    }

                    return View(new List<Producto>());
                }
            }


            [HttpGet]
             public IActionResult About()
        {
            return View();
        }

             [HttpGet]
             public IActionResult Clases() { 
              using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:UrlApi").Value + "Clase/ConsultarClases";

              var response = client.GetAsync(url).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    var datosContenido = JsonSerializer.Deserialize<List<Clase>>((JsonElement)result.Contenido!);
                    return View(datosContenido!.Where(x => x.Cupo > 0 && x.Estado == "Activo").ToList());
                }

                return View(new List<Clase>());
               }
          }


        [HttpGet]
        public IActionResult Blog()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Beneficios()
        {
            return View();
        }
    }
}
