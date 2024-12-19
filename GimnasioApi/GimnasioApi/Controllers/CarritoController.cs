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
    public class CarritoController : ControllerBase
    {
        private readonly IConfiguration _conf;
        public CarritoController(IConfiguration conf)
        {
            _conf = conf;
        }

        [HttpPost]
        [Route("RegistrarCarrito")]
        public IActionResult RegistrarCarrito(Carrito model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Execute("RegistrarCarrito", new { model.IdUsuario, model.ConsecutivoProducto, model.Unidades });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "El producto no se ha actualizado correctamente en su carrito";
                }

                return Ok(respuesta);
            }
        }
        [HttpPost]
        [Route("ConsultarCarrito")]
        public IActionResult ConsultarCarrito(Carrito model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Execute("ConsultarCarrito", new { model.IdUsuario, model.ConsecutivoProducto, model.Unidades });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "El producto no se ha actualizado correctamente en su carrito";
                }

                return Ok(respuesta);
            }
        }

    }
}