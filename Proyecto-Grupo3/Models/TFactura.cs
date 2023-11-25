using System;
using System.Collections.Generic;

namespace Proyecto_Grupo3.Models
{
    public partial class TFactura
    {
        public TFactura()
        {
            TFacturasUsuarios = new HashSet<TFacturasUsuario>();
        }

        public short IdFactura { get; set; }
        public int CodigoFactura { get; set; }
        public DateTime FechaCompra { get; set; }
        public short IdCliente { get; set; }
        public string MetodoPago { get; set; } = null!;
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public int CodigoProducto { get; set; }
        public int CantidadProductos { get; set; }

        public virtual TProductosVendido CodigoProductoNavigation { get; set; } = null!;
        public virtual TRegistroCliente IdClienteNavigation { get; set; } = null!;
        public virtual ICollection<TFacturasUsuario> TFacturasUsuarios { get; set; }
    }
}
