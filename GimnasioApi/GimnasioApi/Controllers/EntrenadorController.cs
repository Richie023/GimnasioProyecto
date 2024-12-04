﻿using Dapper;
using GimnasioApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntrenadorController : ControllerBase
    {
        private readonly IConfiguration _conf;
        public EntrenadorController(IConfiguration conf)
        {
            _conf = conf;
        }

        [HttpPost]
        [Route("CrearEntrenador")]
        public IActionResult CrearEntrenador(Entrenador Model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();

                var result = context.Execute("RegistrarEntrenador", new
                {
                    Model.Identificacion,
                    Model.Nombre,
                    Model.Apellido,
                    Model.Especialidad,
                    Model.Correo,
                    Model.Contrasena,
                    Model.Telefono,
                    Model.Disponibilidad
                });

                if (result > 0)
                {
                    respuesta.Codigo = 0;

                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Su infromacion no se ha registrado correctamente";
                }
                return Ok(respuesta);

            }

        }

        [HttpPost]
        [Route("crearClase")]
        public IActionResult crearClase(Clase Model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();

                var result = context.Execute("RegistrarClase", new
                {
                  
                    Model.Nombre,
                    Model.Descripcion,
                    Model.Nivel,
                    Model.FechaHora,
                    Model.Duracion,
                    Model.IdEntrenador,
                    Model.CapacidadMaxima
                });

                if (result > 0)
                {
                    respuesta.Codigo = 0;

                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Su infromacion no se ha registrado correctamente";
                }
                return Ok(respuesta);

            }

        }



        [HttpGet]
        [Route("ConsultarEntrenadores")]
        public IActionResult ConsultarEntrenadores()
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Query<Entrenador>("ConsultarEntrenadores", new { });

                if (result.Any())
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No hay entrenadores registrados en este momento";
                }

                return Ok(respuesta);
            }
        }


        [HttpGet]
        [Route("ConsultarClases")]
        public IActionResult ConsultarClases()
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Query<Clase>("ConsultarClases", new { });

                if (result.Any())
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No hay clases registrados en este momento";
                }

                return Ok(respuesta);
            }
        }
        private string Encrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_conf.GetSection("Variables:Llave").Value!);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(texto);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }


        private string Decrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(texto);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_conf.GetSection("Variables:Llave").Value!);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        [HttpGet]
        [Route("ConsultarRoles")]
        public IActionResult ConsultarRoles()
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Query<Rol>("ConsultarRoles", new { });

                if (result.Any())
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No hay usuarios registrados en este momento";
                }

                return Ok(respuesta);
            }
        }
    }
}
