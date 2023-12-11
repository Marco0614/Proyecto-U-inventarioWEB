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

        [Required]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; } = null!;

        [Required]
        [Display(Name = "Correo Electronico")]
        public string Correo { get; set; } = null!;

        public virtual ICollection<TFactura> TFacturas { get; set; }
        public virtual ICollection<TFacturasUsuario> TFacturasUsuarios { get; set; }
    }
}
