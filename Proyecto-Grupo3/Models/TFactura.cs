using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Grupo3.Models
{
    public partial class TFactura
    {
        public TFactura()
        {
            TFacturasUsuarios = new HashSet<TFacturasUsuario>();
        }

        [Required]
        [Display(Name = "ID Factura")]
        public short IdFactura { get; set; }

        [Required]
        [Display(Name = "Codigo Factura")]
        public int CodigoFactura { get; set; }

        [Required]
        [Display(Name = "Fecha de Compra")]
        public DateTime FechaCompra { get; set; }

        [Required]
        [Display(Name = "ID Cliente")]
        public short IdCliente { get; set; }

        [Required]
        [Display(Name = "Metodo de Pago")]
        public string MetodoPago { get; set; } = null!;

        [Required]
        [Display(Name = "Subtotal")]
        public decimal Subtotal { get; set; }

        [Required]
        [Display(Name = "IVA")]
        public decimal Iva { get; set; }

        [Required]
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Required]
        [Display(Name = "Codigo Producto")]
        public int CodigoProducto { get; set; }

        [Required]
        [Display(Name = "Cantidad de Productos")]
        public int CantidadProductos { get; set; }

        public virtual TProductosVendido CodigoProductoNavigation { get; set; } = null!;
        public virtual TRegistroCliente IdClienteNavigation { get; set; } = null!;
        public virtual ICollection<TFacturasUsuario> TFacturasUsuarios { get; set; }
    }
}
