using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GimnasioApi.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   public class LoginController : ControllerBase
    {
    private readonly IConfiguration _conf;

    public LoginController(IConfiguration conf)
    {
        _conf = conf;
    }


        [HttpPost]
        [Route("CrearCuenta")]
        public IActionResult CrearCuenta(Usuario Model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Execute("RegistrarUsuario", new
                {
                    Model.Identificacion,
                    Model.Nombre,
                    Model.Apellido,
                    Model.Correo,
                    Model.Contrasena,
                    Model.Telefono
                });

                if (result > 0)
                {
                    respuesta.Codigo = 1;

                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Su infromacion no se ha registrado correctamente";
                }
                return Ok(respuesta);

            }
                
        }
    }
}
