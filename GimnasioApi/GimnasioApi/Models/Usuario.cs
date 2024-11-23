namespace GimnasioApi.Models
{
    public class Usuario
    {
        public long IdUsuario { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public string ConfirmarContrasena { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;
        public bool UsaClaveTemp { get; set; }
        public DateTime Vigencia { get; set; }

        public string Rol { get; set; } = string.Empty;




    }
}
