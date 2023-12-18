using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Grupo3.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Proyecto_Grupo3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            string NombreUsuario = "";

            if (claimUser.Identity.IsAuthenticated) 
            {
                NombreUsuario = claimUser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();
            }

            ViewData["nombreUsuario"] = NombreUsuario;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("IniciarSesion","LogIn");
        }
    }
}