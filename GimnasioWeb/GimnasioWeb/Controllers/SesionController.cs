using GimnasioWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GimnasioWeb.Controllers
{
    public class SesionController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _conf;
        public SesionController(IHttpClientFactory http, IConfiguration conf)
        {
            _http = http;
            _conf = conf;
        }

        [HttpPost]
        public IActionResult RegistrarSesion(long ConsecutivoClase, int Cantidad)
        {
            using (var client = _http.CreateClient())
            {
                var url = _conf.GetSection("Variables:UrlApi").Value + "Sesion/RegistrarSesion";

                var model = new Sesion();
                model.IdUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario")!.ToString());
                model.ConsecutivoClase = ConsecutivoClase;
                model.Cupos = Cantidad;

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PostAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                return Json(result!.Codigo);
            }
        }

    }
}