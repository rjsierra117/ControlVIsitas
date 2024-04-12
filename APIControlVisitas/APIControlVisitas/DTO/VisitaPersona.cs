using System.ComponentModel.DataAnnotations;

namespace APIControlVisitas.DTO
{
    public class VisitaPersona
    {
        public int IdVisitas { get; set; }
        public int? IdResidente { get; set; }
        public int? IdInvitado { get; set; }
        public string Observaciones { get; set; } = null!;
        public byte? Estado { get; set; }
        public string? NombreResidente { get; set; }
        public string? ApellidoResidente { get; set; }
        public string? NoIdentificacionResidente { get; set; }
        public string? ModeloCasa { get; set; }
        public string? ColorCasa { get; set; }
        public string? NombreInvitado { get; set; }
        public string? ApellidoInvitado { get; set; }
        public string? NoIdentificacionInvitado { get; set; }
    }
}
