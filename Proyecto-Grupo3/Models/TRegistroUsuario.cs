using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Grupo3.Models
{
    public partial class TRegistroUsuario
    {
        [Required]
        [Display(Name = "ID Usuario")]
        public short IdUsuario { get; set; }

        [Required]
        [Display(Name = "Identificacion Usuario")]
        public string IdentificacionUsuario { get; set; } = null!;

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El Nombre debe tener al menos 3 caracteres y un máximo de 50 caracteres.")]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; } = null!;

        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El Correo debe tener un formato correcto de correo electrónico.")]
        [Display(Name = "Correo Electronico")]
        public string Correo { get; set; } = null!;

        [Required]
        [Display(Name = "Tipo de Usuario")]
        public string TipoUsuario { get; set; } = null!;

        [Required]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = null!;

        [Required]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; } = null!;
    }
}
