using Microsoft.EntityFrameworkCore;
using Proyecto_Grupo3.Models;

namespace Proyecto_Grupo3.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<TRegistroUsuario> GetUsuario(string correo, string contrasena);

        Task<TRegistroUsuario> GuardarUsuario(TRegistroUsuario modelo);

    }
}
