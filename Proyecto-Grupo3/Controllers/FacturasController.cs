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
    [Authorize(Roles = "Administrador,Vendedor,Contador")]
    public class FacturasController : Controller
    {
        private readonly DB_FARMACIAContext _context;

        public FacturasController(DB_FARMACIAContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            var dB_FARMACIAContext = _context.TFacturas.Include(t => t.CodigoProductoNavigation).Include(t => t.IdClienteNavigation);
            return View(await dB_FARMACIAContext.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.TFacturas == null)
            {
                return NotFound();
            }

            var tFactura = await _context.TFacturas
                .Include(t => t.CodigoProductoNavigation)
                .Include(t => t.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (tFactura == null)
            {
                return NotFound();
            }

            return View(tFactura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            ViewData["CodigoProducto"] = new SelectList(_context.TProductosVendidos, "CodigoProducto", "CodigoProducto");
            ViewData["IdCliente"] = new SelectList(_context.TRegistroClientes, "IdCliente", "IdCliente");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFactura,CodigoFactura,FechaCompra,IdCliente,MetodoPago,Subtotal,Iva,Total,CodigoProducto,CantidadProductos")] TFactura tFactura)
        {
            try 
            {
                _context.Add(tFactura);
                await _context.SaveChangesAsync();
                TempData["success"] = "La factura ha sido creado";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
            ViewData["CodigoProducto"] = new SelectList(_context.TProductosVendidos, "CodigoProducto", "CodigoProducto", tFactura.CodigoProducto);
            ViewData["IdCliente"] = new SelectList(_context.TRegistroClientes, "IdCliente", "IdCliente", tFactura.IdCliente);
            return View(tFactura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.TFacturas == null)
            {
                return NotFound();
            }

            var tFactura = await _context.TFacturas.FindAsync(id);
            if (tFactura == null)
            {
                return NotFound();
            }
            ViewData["CodigoProducto"] = new SelectList(_context.TProductosVendidos, "CodigoProducto", "CodigoProducto", tFactura.CodigoProducto);
            ViewData["IdCliente"] = new SelectList(_context.TRegistroClientes, "IdCliente", "IdCliente", tFactura.IdCliente);
            return View(tFactura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdFactura,CodigoFactura,FechaCompra,IdCliente,MetodoPago,Subtotal,Iva,Total,CodigoProducto,CantidadProductos")] TFactura tFactura)
        {
            if (id != tFactura.IdFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tFactura);
                    await _context.SaveChangesAsync();
                    TempData["edit"] = "La factura ha sido editada";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TFacturaExists(tFactura.IdFactura))
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
            ViewData["CodigoProducto"] = new SelectList(_context.TProductosVendidos, "CodigoProducto", "CodigoProducto", tFactura.CodigoProducto);
            ViewData["IdCliente"] = new SelectList(_context.TRegistroClientes, "IdCliente", "IdCliente", tFactura.IdCliente);
            return View(tFactura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.TFacturas == null)
            {
                return NotFound();
            }

            var tFactura = await _context.TFacturas
                .Include(t => t.CodigoProductoNavigation)
                .Include(t => t.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (tFactura == null)
            {
                return NotFound();
            }

            return View(tFactura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.TFacturas == null)
            {
                return Problem("Entity set 'DB_FARMACIAContext.TFacturas'  is null.");
            }
            var tFactura = await _context.TFacturas.FindAsync(id);
            if (tFactura != null)
            {
                _context.TFacturas.Remove(tFactura);
            }
            
            await _context.SaveChangesAsync();
            TempData["error"] = "La factura ha sido eliminada";
            return RedirectToAction(nameof(Index));
        }

        private bool TFacturaExists(short id)
        {
          return (_context.TFacturas?.Any(e => e.IdFactura == id)).GetValueOrDefault();
        }
    }
}
