using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Grupo3.Models;
using Proyecto_Grupo3.Servicios.Contrato;
using System.Collections.Generic;

namespace Proyecto_Grupo3.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DB_FARMACIAContext _dbFARMACIAContext;

        public UsuarioService(DB_FARMACIAContext dbFARMACIAContext)
        {
            _dbFARMACIAContext = dbFARMACIAContext;
        }

        public async Task<TRegistroUsuario> GetUsuario(string correo, string contrasena)
        {
            TRegistroUsuario usuarioEncontrado = await _dbFARMACIAContext.TRegistroUsuarios.Where(u => u.Correo == correo && u.Contraseña == contrasena).FirstOrDefaultAsync();

            return usuarioEncontrado;
        }

        public async Task<TRegistroUsuario> GuardarUsuario(TRegistroUsuario modelo)
        {
            _dbFARMACIAContext.TRegistroUsuarios.Add(modelo);
            await _dbFARMACIAContext.SaveChangesAsync();
            return modelo;
        }
    }
}
