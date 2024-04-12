﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIControlVisitas.Models;

public partial class Residente
{
    public int IdResidente { get; set; }
    [Required(ErrorMessage = "El campo Primer Nombre es requerido")]
    public int? IdPersona { get; set; }
    [Required(ErrorMessage = "El campo Primer Nombre es requerido")]
    public int? IdCasa { get; set; }
    [Required(ErrorMessage = "El campo Primer Nombre es requerido")]
    public byte? Estado { get; set; }
}
