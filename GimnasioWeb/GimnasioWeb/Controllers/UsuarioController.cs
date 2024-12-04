
﻿using Microsoft.AspNetCore.Mvc;
using GimnasioWeb.Models;
using GimnasioWeb.Servicios;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Reflection;
using System;

namespace GimnasioWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _conf;
        private readonly IMetodosComunes _comunes;
        public UsuarioController(IHttpClientFactory http, IConfiguration conf, IMetodosComunes comunes)
        {
            _http = http;
            _conf = conf;
            _comunes = comunes;
        }


        [HttpGet]
        public IActionResult CambiarContrasenna()
        {
            return View();
        }



        [HttpPost]
        public IActionResult CambiarContrasenna(Usuario model)
        {
            model.Contrasena = _comunes.Encrypt(model.Contrasena);
            model.ConfirmarContrasena = _comunes.Encrypt(model.ConfirmarContrasena);

            if (model.Contrasena != model.ConfirmarContrasena)
            {
                ViewBag.Mensaje = "La confirmación de su contraseña no coincide";
                return View();
            }

            model.IdUsuario = long.Parse(HttpContext.Session.GetString("IdUsuario")!.ToString());

            using (var client = _http.CreateClient())
            {
                var url = _conf.GetSection("Variables:UrlApi").Value + "Usuario/ActualizarContrasenna";

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PutAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    return RedirectToAction("CerrarSesion", "Login");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View();
                }
            }
        }

        [HttpGet]
        public IActionResult ActualizarPerfil()
        {
          var IdUsuario = long.Parse(HttpContext.Session.GetString("IdUsuario")!.ToString());
            return View(ObtenerUsuario(IdUsuario));


        }
        

        [HttpPost]
        public IActionResult ActualizarPerfil(Usuario model)
        {
            model.IdUsuario = long.Parse(HttpContext.Session.GetString("IdUsuario")!.ToString());

            using (var client = _http.CreateClient())
            {
                var url = _conf.GetSection("Variables:UrlApi").Value + "Usuario/ActualizarPerfil";

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PutAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;
                ViewBag.Mensaje = result!.Mensaje;

                if (result != null && result.Codigo == 0)
                {
                    HttpContext.Session.SetString("NombreUsuario", model.Nombre);
                    return View();
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpGet]
        public IActionResult ActualizarUsuario(long IdUsuario)
        {
            ConsultarRoles();
            return View(ObtenerUsuario(IdUsuario));




        }

        [HttpPost]
        public IActionResult ActualizarUsuario(Usuario model)
        {

            using (var client = _http.CreateClient())
            {
                var url = _conf.GetSection("Variables:UrlApi").Value + "Usuario/ActualizarPerfil";

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PutAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {

                    return RedirectToAction("ConsultarUsuarios", "Usuario");
                }
                else
                {
                    ConsultarRoles();
                    ViewBag.Mensaje = result!.Mensaje;

                    return View();
                }
            }
        }

        [HttpGet]
        public IActionResult ConsultarUsuarios()
        {
            
                return View(ObtenerUsuarios());
            }
        


        [HttpPost]
        public IActionResult ActualizarEstado(Usuario model)
        {
            using (var client = _http.CreateClient())
            {
                var url = _conf.GetSection("Variables:UrlApi").Value + "Usuario/ActualizarEstado";

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PutAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    return RedirectToAction("ConsultarUsuarios", "Usuario");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View("ConsultarUsuarios",ObtenerUsuarios());
                }
            }
        }




        public void ConsultarRoles()
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:UrlApi").Value + "Usuario/ConsultarRoles";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;
                if (result != null && result.Codigo == 0)
                {
                    ViewBag.DropDownRoles = JsonSerializer.Deserialize<List<Rol>>((JsonElement)result.Contenido!);
                }
            }
        }

        private Usuario? ObtenerUsuario(long IdUsuario)
        {
            using (var client = _http.CreateClient())
            {

                string url = _conf.GetSection("Variables:UrlApi").Value + "Usuario/ConsultarUsuario?IdUsuario=" + IdUsuario;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    return JsonSerializer.Deserialize<Usuario>((JsonElement)result.Contenido!);
                
                }

                return (new Usuario());
            }
        }

        private List<Usuario> ObtenerUsuarios()
        {
            var IdUsuario = long.Parse(HttpContext.Session.GetString("IdUsuario")!.ToString());
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:UrlApi").Value + "Usuario/ConsultarUsuarios";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));

                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    var datosContenido = JsonSerializer.Deserialize<List<Usuario>>((JsonElement)result.Contenido!);
                    return datosContenido!.Where(x => x.IdUsuario != IdUsuario).ToList();
                }

                return (new List<Usuario>());
            }
        }



       
    }
}
