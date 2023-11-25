using System;
using System.Collections.Generic;

namespace Proyecto_Grupo3.Models
{
    public partial class TRegistroCliente
    {
        public TRegistroCliente()
        {
            TFacturas = new HashSet<TFactura>();
            TFacturasUsuarios = new HashSet<TFacturasUsuario>();
        }

        public short IdCliente { get; set; }
        public string IdentificacionCliente { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string Correo { get; set; } = null!;

        public virtual ICollection<TFactura> TFacturas { get; set; }
        public virtual ICollection<TFacturasUsuario> TFacturasUsuarios { get; set; }
    }
}
