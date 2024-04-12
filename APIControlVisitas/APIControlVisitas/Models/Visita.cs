using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIControlVisitas.Models;

public partial class Visita
{
    public int IdVisitas { get; set; }
    [Required(ErrorMessage = "El campo Primer Nombre es requerido")]
    public int? IdResidente { get; set; }
    [Required(ErrorMessage = "El campo Primer Nombre es requerido")]
    public int? IdInvitado { get; set; }
    [Required(ErrorMessage = "El campo Primer Nombre es requerido")]
    [StringLength(maximumLength: 150, ErrorMessage = "El campo no debe tener más de 150 carácteres")]
    public string Observaciones { get; set; } = null!;
    [Required(ErrorMessage = "El campo Primer Nombre es requerido")]
    public byte? Estado { get; set; }
}
