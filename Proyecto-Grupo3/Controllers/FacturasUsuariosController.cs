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
    public class FacturasUsuariosController : Controller
    {
        private readonly DB_FARMACIAContext _context;

        public FacturasUsuariosController(DB_FARMACIAContext context)
        {
            _context = context;
        }

        // GET: FacturasUsuarios
        public async Task<IActionResult> Index()
        {
            var dB_FARMACIAContext = _context.TFacturasUsuarios.Include(t => t.IdClienteNavigation).Include(t => t.IdFacturaNavigation);
            return View(await dB_FARMACIAContext.ToListAsync());
        }

        // GET: FacturasUsuarios/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.TFacturasUsuarios == null)
            {
                return NotFound();
            }

            var tFacturasUsuario = await _context.TFacturasUsuarios
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdFacturaNavigation)
                .FirstOrDefaultAsync(m => m.IdFacturasUsuario == id);
            if (tFacturasUsuario == null)
            {
                return NotFound();
            }

            return View(tFacturasUsuario);
        }

        // GET: FacturasUsuarios/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.TRegistroClientes, "IdCliente", "IdCliente");
            ViewData["IdFactura"] = new SelectList(_context.TFacturas, "IdFactura", "IdFactura");
            return View();
        }

        // POST: FacturasUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFacturasUsuario,IdFactura,IdCliente")] TFacturasUsuario tFacturasUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tFacturasUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.TRegistroClientes, "IdCliente", "IdCliente", tFacturasUsuario.IdCliente);
            ViewData["IdFactura"] = new SelectList(_context.TFacturas, "IdFactura", "IdFactura", tFacturasUsuario.IdFactura);
            return View(tFacturasUsuario);
        }

        // GET: FacturasUsuarios/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.TFacturasUsuarios == null)
            {
                return NotFound();
            }

            var tFacturasUsuario = await _context.TFacturasUsuarios.FindAsync(id);
            if (tFacturasUsuario == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.TRegistroClientes, "IdCliente", "IdCliente", tFacturasUsuario.IdCliente);
            ViewData["IdFactura"] = new SelectList(_context.TFacturas, "IdFactura", "IdFactura", tFacturasUsuario.IdFactura);
            return View(tFacturasUsuario);
        }

        // POST: FacturasUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdFacturasUsuario,IdFactura,IdCliente")] TFacturasUsuario tFacturasUsuario)
        {
            if (id != tFacturasUsuario.IdFacturasUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tFacturasUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TFacturasUsuarioExists(tFacturasUsuario.IdFacturasUsuario))
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
            ViewData["IdCliente"] = new SelectList(_context.TRegistroClientes, "IdCliente", "IdCliente", tFacturasUsuario.IdCliente);
            ViewData["IdFactura"] = new SelectList(_context.TFacturas, "IdFactura", "IdFactura", tFacturasUsuario.IdFactura);
            return View(tFacturasUsuario);
        }

        // GET: FacturasUsuarios/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.TFacturasUsuarios == null)
            {
                return NotFound();
            }

            var tFacturasUsuario = await _context.TFacturasUsuarios
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdFacturaNavigation)
                .FirstOrDefaultAsync(m => m.IdFacturasUsuario == id);
            if (tFacturasUsuario == null)
            {
                return NotFound();
            }

            return View(tFacturasUsuario);
        }

        // POST: FacturasUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.TFacturasUsuarios == null)
            {
                return Problem("Entity set 'DB_FARMACIAContext.TFacturasUsuarios'  is null.");
            }
            var tFacturasUsuario = await _context.TFacturasUsuarios.FindAsync(id);
            if (tFacturasUsuario != null)
            {
                _context.TFacturasUsuarios.Remove(tFacturasUsuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TFacturasUsuarioExists(short id)
        {
          return (_context.TFacturasUsuarios?.Any(e => e.IdFacturasUsuario == id)).GetValueOrDefault();
        }
    }
}
