namespace GimnasioApi.Models
{
    public class Clase
    {
        public long Consecutivo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public string Entrenador { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Cupo { get; set; }
        public string Imagen { get; set; } = string.Empty;

        public DateTime Horario { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
