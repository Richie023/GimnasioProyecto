using GimnasioWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GimnasioWeb.Controllers
{
    public class ClaseController : Controller
    {
        // GET: ClaseController

            private readonly IHttpClientFactory _http;
            private readonly IConfiguration _conf;
            private readonly IHostEnvironment _env;
            public ClaseController(IHttpClientFactory http, IConfiguration conf, IHostEnvironment env)
            {
                _http = http;
                _conf = conf;
                _env = env;
            }

            [HttpGet]
            public IActionResult ConsultarClases()
            {
                return View(ObtenerClases());
            }


            [HttpPost]
            public IActionResult ActualizarEstadoClase(Clase model)
            {
                using (var client = _http.CreateClient())
                {
                    var url = _conf.GetSection("Variables:UrlApi").Value + "Clase/ActualizarEstadoClase";

                    JsonContent datos = JsonContent.Create(model);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                    var response = client.PutAsync(url, datos).Result;
                    var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                    if (result != null && result.Codigo == 0)
                    {
                        return RedirectToAction("ConsultarClases", "Clase");
                    }
                    else
                    {
                        ViewBag.Mensaje = result!.Mensaje;
                        return View("ConsultarClases", ObtenerClases());
                    }
                }
            }
       

        [HttpGet]
        public IActionResult RegistrarClase()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarClase(IFormFile ImagenClase, Clase model)
        {
            var ext = string.Empty;
            var folder = string.Empty;

            if (ImagenClase != null)
            {
                ext = Path.GetExtension(Path.GetFileName(ImagenClase.FileName));
                folder = Path.Combine(_env.ContentRootPath, "wwwroot\\class");
                model.Imagen = "/class/";

                if (ext.ToLower() != ".png")
                {
                    ViewBag.Mensaje = "La imagen debe ser .png";
                    return View();
                }
            }

            using (var client = _http.CreateClient())
            {
                var url = _conf.GetSection("Variables:UrlApi").Value + "Clase/RegistrarClase";

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PostAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    if (ImagenClase != null)
                    {
                        var archivo = Path.Combine(folder, result.Mensaje + ext);
                        using (Stream fs = new FileStream(archivo, FileMode.Create))
                        {
                            ImagenClase.CopyTo(fs);
                        }
                    }

                    return RedirectToAction("ConsultarClases", "Clase");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View();
                }
            }
        }
        [HttpGet]
        public IActionResult ActualizarClase(long Consecutivo)
        {
            return View(ObtenerClase(Consecutivo));
        }

        [HttpPost]
        public IActionResult ActualizarClase(IFormFile ImagenClase, Clase model)
        {
            var ext = string.Empty;
            var folder = string.Empty;

            if (ImagenClase != null)
            {
                ext = Path.GetExtension(Path.GetFileName(ImagenClase.FileName));
                folder = Path.Combine(_env.ContentRootPath, "wwwroot\\class");

                if (ext.ToLower() != ".png")
                {
                    ViewBag.Mensaje = "La imagen debe ser .png";
                    return View();
                }
            }

            using (var client = _http.CreateClient())
            {
                var url = _conf.GetSection("Variables:UrlApi").Value + "Clase/ActualizarClase";

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PutAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    if (ImagenClase != null)
                    {
                        var archivo = Path.Combine(folder, model.Consecutivo + ext);
                        using (Stream fs = new FileStream(archivo, FileMode.Create))
                        {
                            ImagenClase.CopyTo(fs);
                        }
                    }

                    return RedirectToAction("ConsultarClases", "Clase");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View();
                }
            }
        }

        private List<Clase> ObtenerClases()
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:UrlApi").Value + "Clase/ConsultarClases";

             
                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    var datosContenido = JsonSerializer.Deserialize<List<Clase>>((JsonElement)result.Contenido!);
                    return datosContenido!;
                }

                return new List<Clase>();
            }
        }

        private Clase? ObtenerClase(long Consecutivo)
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:UrlApi").Value + "Clase/ConsultarClase?Consecutivo=" + Consecutivo;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    return JsonSerializer.Deserialize<Clase>((JsonElement)result.Contenido!);
                }

                return new Clase();
            }
        }


    }
}
  
    
