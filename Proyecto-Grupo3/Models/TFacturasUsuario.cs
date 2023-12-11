using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Grupo3.Models
{
    public partial class TFacturasUsuario
    {
        [Required]
        [Display(Name = "ID Facturas - Usuario")]
        public short IdFacturasUsuario { get; set; }

        [Required]
        [Display(Name = "ID Factura")]
        public short IdFactura { get; set; }

        [Required]
        [Display(Name = "ID Cliente")]
        public short IdCliente { get; set; }

        public virtual TRegistroCliente IdClienteNavigation { get; set; } = null!;
        public virtual TFactura IdFacturaNavigation { get; set; } = null!;
    }
}
