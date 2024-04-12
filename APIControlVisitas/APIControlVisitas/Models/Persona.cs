using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIControlVisitas.Models;

public partial class Persona
{
    public int IdPersona { get; set; }
    [Required(ErrorMessage = "El campo Nombre es requerido")]
    [StringLength(maximumLength: 50, ErrorMessage = "El campo no debe tener más de 50 carácteres")]
    public string? Nombre { get; set; }
    [Required(ErrorMessage = "El campo Apellido es requerido")]
    [StringLength(maximumLength: 50, ErrorMessage = "El campo no debe tener más de 50 carácteres")]
    public string? Apellido { get; set; }
    [Required(ErrorMessage = "El campo No Identificación es requerido")]
    [StringLength(maximumLength: 50, ErrorMessage = "El campo no debe tener más de 50 carácteres")]
    public string? NoIdentificacion { get; set; }
    [Required(ErrorMessage = "El campo Primer Nombre es requerido")]
    public bool? EsExtranjero { get; set; }
    [Required(ErrorMessage = "El campo Primer Nombre es requerido")]
    public byte? Genero { get; set; }
    [Required(ErrorMessage = "El campo Primer Nombre es requerido")]
    public byte? Estado { get; set; }

}
