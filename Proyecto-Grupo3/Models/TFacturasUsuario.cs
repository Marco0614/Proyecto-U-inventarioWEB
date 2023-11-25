using System;
using System.Collections.Generic;

namespace Proyecto_Grupo3.Models
{
    public partial class TFacturasUsuario
    {
        public short IdFacturasUsuario { get; set; }
        public short IdFactura { get; set; }
        public short IdCliente { get; set; }

        public virtual TRegistroCliente IdClienteNavigation { get; set; } = null!;
        public virtual TFactura IdFacturaNavigation { get; set; } = null!;
    }
}
