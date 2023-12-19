using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Grupo3.Models;

namespace Proyecto_Grupo3.Controllers
{
    [Authorize(Roles = "Administrador,Vendedor")]
    public class RegistroClientesController : Controller
    {
        private readonly DB_FARMACIAContext _context;

        public RegistroClientesController(DB_FARMACIAContext context)
        {
            _context = context;
        }

        // GET: RegistroClientes
        public async Task<IActionResult> Index()
        {
              return _context.TRegistroClientes != null ? 
                          View(await _context.TRegistroClientes.ToListAsync()) :
                          Problem("Entity set 'DB_FARMACIAContext.TRegistroClientes'  is null.");
        }

        // GET: RegistroClientes/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.TRegistroClientes == null)
            {
                return NotFound();
            }

            var tRegistroCliente = await _context.TRegistroClientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (tRegistroCliente == null)
            {
                return NotFound();
            }

            return View(tRegistroCliente);
        }

        // GET: RegistroClientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegistroClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,IdentificacionCliente,NombreCompleto,Correo")] TRegistroCliente tRegistroCliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tRegistroCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tRegistroCliente);
        }

        // GET: RegistroClientes/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.TRegistroClientes == null)
            {
                return NotFound();
            }

            var tRegistroCliente = await _context.TRegistroClientes.FindAsync(id);
            if (tRegistroCliente == null)
            {
                return NotFound();
            }
            return View(tRegistroCliente);
        }

        // POST: RegistroClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdCliente,IdentificacionCliente,NombreCompleto,Correo")] TRegistroCliente tRegistroCliente)
        {
            if (id != tRegistroCliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tRegistroCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TRegistroClienteExists(tRegistroCliente.IdCliente))
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
            return View(tRegistroCliente);
        }

        // GET: RegistroClientes/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.TRegistroClientes == null)
            {
                return NotFound();
            }

            var tRegistroCliente = await _context.TRegistroClientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (tRegistroCliente == null)
            {
                return NotFound();
            }

            return View(tRegistroCliente);
        }

        // POST: RegistroClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.TRegistroClientes == null)
            {
                return Problem("Entity set 'DB_FARMACIAContext.TRegistroClientes'  is null.");
            }
            var tRegistroCliente = await _context.TRegistroClientes.FindAsync(id);
            if (tRegistroCliente != null)
            {
                _context.TRegistroClientes.Remove(tRegistroCliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TRegistroClienteExists(short id)
        {
          return (_context.TRegistroClientes?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
