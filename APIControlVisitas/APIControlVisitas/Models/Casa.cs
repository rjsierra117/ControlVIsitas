using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIControlVisitas.Models;

public partial class Casa
{
    public int IdCasa { get; set; }
    [Required(ErrorMessage = "El campo Modelo es requerido")]
    [StringLength(maximumLength: 50, ErrorMessage = "El campo no debe tener más de 50 carácteres")]
    public string? Modelo { get; set; }
    [Required(ErrorMessage = "El color es requerido")]
    [StringLength(maximumLength: 50, ErrorMessage = "El campo no debe tener más de 50 carácteres")]
    public string? Color { get; set; }
    [Required(ErrorMessage = "El campo Primer Nombre es requerido")]
    public byte? Estado { get; set; }
}
