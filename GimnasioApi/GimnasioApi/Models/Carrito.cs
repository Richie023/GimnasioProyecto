namespace GimnasioApi.Models
{
    public class Carrito
    {
        public long IdUsuario { get; set; }
        public long ConsecutivoProducto { get; set; }
        public int Unidades { get; set; }
        public DateTime Fecha { get; set; }
    }
}
