using Dapper;
using GimnasioApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionController : ControllerBase
    {

        private readonly IConfiguration _conf;
        public SesionController(IConfiguration conf)
        {
            _conf = conf;
        }

        [HttpPost]
        [Route("RegistrarSesion")]
        public IActionResult RegistrarSesion(Sesion model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Execute("RegistrarSesion", new { model.IdUsuario, model.ConsecutivoClase, model.Cupos });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "La sesion no se ha actualizado correctamente en su carrito";
                }

                return Ok(respuesta);
            }
        }

    }
}