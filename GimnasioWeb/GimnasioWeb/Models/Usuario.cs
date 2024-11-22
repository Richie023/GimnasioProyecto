namespace GimnasioWeb.Models
{
    public class Usuario
    {
        public long IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;

        public string Telefono  { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }

        public string ConfirmarContrasena { get; set; } = string.Empty;



    }
}
