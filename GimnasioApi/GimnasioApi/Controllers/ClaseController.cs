using Dapper;
using GimnasioApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace GimnasioApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClaseController : ControllerBase

{
    private readonly IConfiguration _conf;
    public ClaseController(IConfiguration conf)
    {
        _conf = conf;
    }

        [AllowAnonymous]
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
                    respuesta.Mensaje = "No hay clases registradas en este momento";
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("ConsultarClase")]
        public IActionResult ConsultarClase(int Consecutivo)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.QueryFirstOrDefault<Clase>("ConsultarClase", new { Consecutivo });

                if (result != null)
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No hay Clases registradas en este momento";
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ActualizarEstadoClase")]
        public IActionResult ActualizarEstadoClase(Clase model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();

                var result = context.Execute("ActualizarEstadoClase", new
                {
                    model.Consecutivo
                });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "El estado de la clase no se ha actualizado correctamente";
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("RegistrarClase")]
        public IActionResult RegistrarClase(Clase model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.QueryFirstOrDefault<Producto>("RegistrarClase", new { model.Nombre,model.Descripcion,model.Entrenador, model.Precio, model.Cupo,model.Horario ,model.Imagen });

                if (result != null)
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = result.Consecutivo.ToString();
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "La información del producto no se ha registrado correctamente";
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ActualizarClase")]
        public IActionResult ActualizarClase(Clase model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();

                var result = context.Execute("ActualizarClase", new
                {
                    model.Consecutivo,
                    model.Nombre,
                    model.Descripcion,
                    model.Entrenador,
                    model.Precio,
                    model.Cupo,
                    model.Horario,
                 
                });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "La información del producto no se ha actualizado correctamente";
                }

                return Ok(respuesta);
            }
        }


    }
}
    

