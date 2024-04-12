using System.ComponentModel.DataAnnotations;

namespace APIControlVisitas.DTO
{
    public class InvitadoPesona
    {
        public int IdInvitado { get; set; }

        public byte? Estado { get; set; }
        public int IdPersona { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? NoIdentificacion { get; set; }
        public bool? EsExtranjero { get; set; }
        public byte? Genero { get; set; }
        public byte? EstadoPersona { get; set; }
    }
}
