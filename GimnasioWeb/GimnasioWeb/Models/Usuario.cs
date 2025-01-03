﻿namespace GimnasioWeb.Models
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
        public string ConfirmarContrasena { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }

        public int Rol { get; set; }

        public string NombreRol { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;



    }
}
