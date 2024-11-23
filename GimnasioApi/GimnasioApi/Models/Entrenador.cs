using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GimnasioApi.Models
{
    public class Entrenador
    {

        public long IdEntrenador { get; set; }

        public string Identificacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        public string Contrasena { get; set; } = string.Empty;
        public string Disponibilidad { get; set; } = string.Empty;
       
      


    }
}
