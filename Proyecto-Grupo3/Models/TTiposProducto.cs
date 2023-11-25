using System;
using System.Collections.Generic;

namespace Proyecto_Grupo3.Models
{
    public partial class TTiposProducto
    {
        public TTiposProducto()
        {
            TProductos = new HashSet<TProducto>();
            TProductosVendidos = new HashSet<TProductosVendido>();
        }

        public int CodigoTipoProducto { get; set; }
        public string DescripcionTipoProducto { get; set; } = null!;

        public virtual ICollection<TProducto> TProductos { get; set; }
        public virtual ICollection<TProductosVendido> TProductosVendidos { get; set; }
    }
}
