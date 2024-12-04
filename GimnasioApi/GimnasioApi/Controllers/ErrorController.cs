using Dapper;
using GimnasioApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly IConfiguration _conf;
        public ErrorController(IConfiguration conf)
        {
            _conf = conf;
        }

        [Route("RegistrarError")]
        public IActionResult RegistrarError()
        {
            var exc = HttpContext.Features.Get<IExceptionHandlerFeature>();

            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var Consecutivo = ObtenerUsuarioToken();
                var Mensaje = exc!.Error.Message;
                var Origen = exc.Path;

                context.Execute("RegistrarError", new { Consecutivo, Mensaje, Origen });

                var respuesta = new Respuesta();
                respuesta.Codigo = -1;
                respuesta.Mensaje = "Se presentó un problema en el sistema";
                return Ok(respuesta);
            }

        }

        private long ObtenerUsuarioToken()
        {
            if (User.Claims.Count() > 0)
            {
                var Consecutivo = User.Claims.Select(Claim => new { Claim.Type, Claim.Value })
                    .FirstOrDefault(x => x.Type == "IdUsuario")!.Value;

                return long.Parse(Consecutivo);
            }

            return 0;
        }

    }
}
    

