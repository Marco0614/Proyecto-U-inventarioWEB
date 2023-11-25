using System;
using System.Collections.Generic;

namespace Proyecto_Grupo3.Models
{
    public partial class TProducto
    {
        public string NombreCategoria { get; set; } = null!;
        public int CodigoTipoProducto { get; set; }
        public string NombreProducto { get; set; } = null!;

        public virtual TTiposProducto CodigoTipoProductoNavigation { get; set; } = null!;
    }
}
