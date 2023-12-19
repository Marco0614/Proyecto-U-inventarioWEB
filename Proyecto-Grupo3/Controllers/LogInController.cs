using Microsoft.AspNetCore.Mvc;

using Proyecto_Grupo3.Models;
using Proyecto_Grupo3.Servicios.Contrato;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Proyecto_Grupo3.Controllers
{
    public class LogInController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;

        public LogInController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string contrasena)
        {
            TRegistroUsuario UsuarioEncontrado = await _usuarioServicio.GetUsuario(correo, contrasena);

            if (UsuarioEncontrado == null)
            {
                ViewData["Mensaje"] = "No se ha encontrado al usuario ... Por favor debe de registrarse";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, UsuarioEncontrado.NombreCompleto),
                new Claim(ClaimTypes.Role, UsuarioEncontrado.TipoUsuario)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(TRegistroUsuario modelo)
        {
            TRegistroUsuario UsuarioCreado = await _usuarioServicio.GuardarUsuario(modelo);

            if (UsuarioCreado.IdUsuario > 0)

                return RedirectToAction("IniciarSesion", "LogIn");

            return RedirectToAction("RegistroUsuarios", "Create");

        }
    }
}
