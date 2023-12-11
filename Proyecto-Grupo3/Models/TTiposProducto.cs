using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Grupo3.Models
{
    public partial class TTiposProducto
    {
        public TTiposProducto()
        {
            TProductos = new HashSet<TProducto>();
            TProductosVendidos = new HashSet<TProductosVendido>();
        }

        [Required]
        [Display(Name = "Codigo de Tipo Producto")]
        public int CodigoTipoProducto { get; set; }

        [Required]
        [Display(Name = "Descripcion de Tipo Producto")]
        public string DescripcionTipoProducto { get; set; } = null!;

        public virtual ICollection<TProducto> TProductos { get; set; }
        public virtual ICollection<TProductosVendido> TProductosVendidos { get; set; }
    }
}
