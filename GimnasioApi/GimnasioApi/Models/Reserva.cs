namespace GimnasioApi.Models
{
    public class Reserva
    {
        public int IdReserva { get; set; } // ID único de la reserva
        public int IdUsuario { get; set; } // ID del usuario que realizó la reserva
        public int IdProducto { get; set; } // ID del producto o servicio reservado
        public DateTime FechaReserva { get; set; } // Fecha en la que se realizó la reserva
        public DateTime FechaInicio { get; set; } // Fecha de inicio de la reserva (para servicios con fechas específicas)
        public DateTime FechaFin { get; set; } // Fecha de fin de la reserva (si aplica)
        public string EstadoReserva { get; set; } // Estado de la reserva (ejemplo: "Confirmada", "Pendiente", "Cancelada")
        public decimal Precio { get; set; } // Precio de la reserva, si aplica
        public string Comentarios { get; set; } // Comentarios adicionales sobre la reserva
    }
}
