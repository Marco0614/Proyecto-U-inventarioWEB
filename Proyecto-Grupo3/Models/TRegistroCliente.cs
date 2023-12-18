using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Grupo3.Models
{
    public partial class TRegistroCliente
    {
        public TRegistroCliente()
        {
            TFacturas = new HashSet<TFactura>();
            TFacturasUsuarios = new HashSet<TFacturasUsuario>();
        }

        [Required]
        [Display(Name = "ID Cliente")]
        public short IdCliente { get; set; }

        [Required]
        [Display(Name = "Identificacion Cliente")]
        public string IdentificacionCliente { get; set; } = null!;

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El Nombre debe tener al menos 3 caracteres y un máximo de 50 caracteres.")]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; } = null!;

        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El Correo debe tener un formato correcto de correo electrónico.")]
        [Display(Name = "Correo Electronico")]
        public string Correo { get; set; } = null!;

        public virtual ICollection<TFactura> TFacturas { get; set; }
        public virtual ICollection<TFacturasUsuario> TFacturasUsuarios { get; set; }
    }
}
