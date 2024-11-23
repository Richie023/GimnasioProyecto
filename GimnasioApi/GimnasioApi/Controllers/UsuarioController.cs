using Dapper;
using GimnasioApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _conf;
        public UsuarioController(IConfiguration conf)
        {
            _conf = conf;
        }

        [HttpPut]
        [Route("ActualizarContrasenna")]
        public IActionResult ActualizarContrasenna(Usuario model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();

                var UsaClaveTemp = false;
                var Vigencia = DateTime.Now;
                var result = context.Execute("ActualizarContrasena", new { model.IdUsuario, model.Contrasena, UsaClaveTemp, Vigencia });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Su información de acceso no se ha actualizado correctamente";
                }

                return Ok(respuesta);
            }
        }
        
      

    }
}