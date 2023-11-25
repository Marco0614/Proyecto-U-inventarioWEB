using System;
using System.Collections.Generic;

namespace Proyecto_Grupo3.Models
{
    public partial class TProductosVendido
    {
        public TProductosVendido()
        {
            TFacturas = new HashSet<TFactura>();
        }

        public int CodigoProducto { get; set; }
        public int CodigoTipoProducto { get; set; }
        public string DescripcionProducto { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Estado { get; set; } = null!;
        public int Cantidad { get; set; }

        public virtual TTiposProducto CodigoTipoProductoNavigation { get; set; } = null!;
        public virtual ICollection<TFactura> TFacturas { get; set; }
    }
}
