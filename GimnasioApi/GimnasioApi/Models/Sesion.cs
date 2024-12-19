namespace GimnasioApi.Models
{
    public class Sesion
    {
        public long IdUsuario { get; set; }
        public long ConsecutivoClase { get; set; }
        public int Cupos { get; set; }
        public DateTime Fecha { get; set; }
    }
}
