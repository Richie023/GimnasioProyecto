﻿namespace GimnasioWeb.Models
{
    public class Usuario
    {
        public string Nombre { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;

        public int Telefono  { get; set; }

        public DateTime FechaRegistro { get; set; }



    }
}
