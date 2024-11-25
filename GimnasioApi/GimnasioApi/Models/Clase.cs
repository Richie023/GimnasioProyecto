namespace GimnasioApi.Models
{
    public class Clase
    {
        public long IdClase { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
        public string Entrenador { get; set; } = string.Empty;

        public DateTime FechaHora { get; set; }
        public int Duracion { get; set; }
        public int IdEntrenador { get; set; }

        public int CapacidadMaxima { get; set; }
        public bool Estado { get; set; }
    }
}
