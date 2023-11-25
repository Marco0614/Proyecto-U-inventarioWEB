using System;
using System.Collections.Generic;

namespace Proyecto_Grupo3.Models
{
    public partial class TRegistroUsuario
    {
        public short IdUsuario { get; set; }
        public string IdentificacionUsuario { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string TipoUsuario { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
    }
}
