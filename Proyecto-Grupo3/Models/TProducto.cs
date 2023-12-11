using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Grupo3.Models
{
    public partial class TProducto
    {
        [Required]
        [Display(Name = "Nombre Categoria")]
        public string NombreCategoria { get; set; } = null!;

        [Required]
        [Display(Name = "Codigo Tipo Producto")]
        public int CodigoTipoProducto { get; set; }

        [Required]
        [Display(Name = "Nombre Producto")]
        public string NombreProducto { get; set; } = null!;

        public virtual TTiposProducto CodigoTipoProductoNavigation { get; set; } = null!;
    }
}
