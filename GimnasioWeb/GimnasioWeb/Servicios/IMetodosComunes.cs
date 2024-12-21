using GimnasioWeb.Models;

namespace GimnasioWeb.Servicios
{
    public interface IMetodosComunes
    {
        string Encrypt(string texto);
        List<Carrito> ConsultarCarrito();

    }
}
