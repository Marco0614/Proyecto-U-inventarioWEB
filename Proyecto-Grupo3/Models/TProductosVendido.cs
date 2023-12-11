using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Grupo3.Models
{
    public partial class TProductosVendido
    {
        public TProductosVendido()
        {
            TFacturas = new HashSet<TFactura>();
        }

        [Required]
        [Display(Name = "Codigo Producto")]
        public int CodigoProducto { get; set; }

        [Required]
        [Display(Name = "Codigo Tipo Producto")]
        public int CodigoTipoProducto { get; set; }

        [Required]
        [Display(Name = "Descripcion Producto")]
        public string DescripcionProducto { get; set; } = null!;

        [Required]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = null!;

        [Required]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        public virtual TTiposProducto CodigoTipoProductoNavigation { get; set; } = null!;
        public virtual ICollection<TFactura> TFacturas { get; set; }
    }
}
