using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Grupo3.Models;

namespace Proyecto_Grupo3.Controllers
{
    public class RegistroUsuariosController : Controller
    {
        private readonly DB_FARMACIAContext _context;

        public RegistroUsuariosController(DB_FARMACIAContext context)
        {
            _context = context;
        }

        // GET: RegistroUsuarios
        public async Task<IActionResult> Index()
        {
              return _context.TRegistroUsuarios != null ? 
                          View(await _context.TRegistroUsuarios.ToListAsync()) :
                          Problem("Entity set 'DB_FARMACIAContext.TRegistroUsuarios'  is null.");
        }

        // GET: RegistroUsuarios/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.TRegistroUsuarios == null)
            {
                return NotFound();
            }

            var tRegistroUsuario = await _context.TRegistroUsuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tRegistroUsuario == null)
            {
                return NotFound();
            }

            return View(tRegistroUsuario);
        }

        // GET: RegistroUsuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegistroUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,IdentificacionUsuario,NombreCompleto,Correo,TipoUsuario,Estado,Contraseña")] TRegistroUsuario tRegistroUsuario)
        {

            if (ModelState.IsValid)
            {
                _context.Add(tRegistroUsuario);
                await _context.SaveChangesAsync();
                TempData["success"] = "El usuario ha sido creado";
                return RedirectToAction(nameof(Index));
            }
            return View(tRegistroUsuario);
        }

        // GET: RegistroUsuarios/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.TRegistroUsuarios == null)
            {
                return NotFound();
            }

            var tRegistroUsuario = await _context.TRegistroUsuarios.FindAsync(id);
            if (tRegistroUsuario == null)
            {
                return NotFound();
            }
            return View(tRegistroUsuario);
        }

        // POST: RegistroUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdUsuario,IdentificacionUsuario,NombreCompleto,Correo,TipoUsuario,Estado,Contraseña")] TRegistroUsuario tRegistroUsuario)
        {
            if (id != tRegistroUsuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tRegistroUsuario);
                    await _context.SaveChangesAsync();
                    TempData["edit"] = "El usuario ha sido editado";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TRegistroUsuarioExists(tRegistroUsuario.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tRegistroUsuario);
        }

        // GET: RegistroUsuarios/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.TRegistroUsuarios == null)
            {
                return NotFound();
            }

            var tRegistroUsuario = await _context.TRegistroUsuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tRegistroUsuario == null)
            {
                return NotFound();
            }

            return View(tRegistroUsuario);
        }

        // POST: RegistroUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.TRegistroUsuarios == null)
            {
                return Problem("Entity set 'DB_FARMACIAContext.TRegistroUsuarios'  is null.");
            }
            var tRegistroUsuario = await _context.TRegistroUsuarios.FindAsync(id);
            if (tRegistroUsuario != null)
            {
                _context.TRegistroUsuarios.Remove(tRegistroUsuario);
            }
            
            await _context.SaveChangesAsync();
            TempData["error"] = "El usuario ha sido eliminado";
            return RedirectToAction(nameof(Index));
        }

        private bool TRegistroUsuarioExists(short id)
        {
          return (_context.TRegistroUsuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
