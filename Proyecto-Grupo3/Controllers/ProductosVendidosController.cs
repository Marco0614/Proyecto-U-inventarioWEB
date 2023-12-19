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
    public class ProductosVendidosController : Controller
    {
        private readonly DB_FARMACIAContext _context;

        public ProductosVendidosController(DB_FARMACIAContext context)
        {
            _context = context;
        }

        // GET: ProductosVendidos
        public async Task<IActionResult> Index()
        {
            var dB_FARMACIAContext = _context.TProductosVendidos.Include(t => t.CodigoTipoProductoNavigation);
            return View(await dB_FARMACIAContext.ToListAsync());
        }

        // GET: ProductosVendidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TProductosVendidos == null)
            {
                return NotFound();
            }

            var tProductosVendido = await _context.TProductosVendidos
                .Include(t => t.CodigoTipoProductoNavigation)
                .FirstOrDefaultAsync(m => m.CodigoProducto == id);
            if (tProductosVendido == null)
            {
                return NotFound();
            }

            return View(tProductosVendido);
        }

        // GET: ProductosVendidos/Create
        public IActionResult Create()
        {
            ViewData["CodigoTipoProducto"] = new SelectList(_context.TTiposProductos, "CodigoTipoProducto", "CodigoTipoProducto");
            return View();
        }

        // POST: ProductosVendidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoProducto,CodigoTipoProducto,DescripcionProducto,Precio,Estado,Cantidad")] TProductosVendido tProductosVendido)
        {
            try 
            {
                _context.Add(tProductosVendido);
                await _context.SaveChangesAsync();
                TempData["success"] = "El producto ha sido agregado al inventario";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
            ViewData["CodigoTipoProducto"] = new SelectList(_context.TTiposProductos, "CodigoTipoProducto", "CodigoTipoProducto", tProductosVendido.CodigoTipoProducto);
            return View(tProductosVendido);
        }

        // GET: ProductosVendidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TProductosVendidos == null)
            {
                return NotFound();
            }

            var tProductosVendido = await _context.TProductosVendidos.FindAsync(id);
            if (tProductosVendido == null)
            {
                return NotFound();
            }
            ViewData["CodigoTipoProducto"] = new SelectList(_context.TTiposProductos, "CodigoTipoProducto", "CodigoTipoProducto", tProductosVendido.CodigoTipoProducto);
            return View(tProductosVendido);
        }

        // POST: ProductosVendidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoProducto,CodigoTipoProducto,DescripcionProducto,Precio,Estado,Cantidad")] TProductosVendido tProductosVendido)
        {
            if (id != tProductosVendido.CodigoProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tProductosVendido);
                    await _context.SaveChangesAsync();
                    TempData["edit"] = "El producto ha sido editado";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TProductosVendidoExists(tProductosVendido.CodigoProducto))
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
            ViewData["CodigoTipoProducto"] = new SelectList(_context.TTiposProductos, "CodigoTipoProducto", "CodigoTipoProducto", tProductosVendido.CodigoTipoProducto);
            return View(tProductosVendido);
        }

        // GET: ProductosVendidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TProductosVendidos == null)
            {
                return NotFound();
            }

            var tProductosVendido = await _context.TProductosVendidos
                .Include(t => t.CodigoTipoProductoNavigation)
                .FirstOrDefaultAsync(m => m.CodigoProducto == id);
            if (tProductosVendido == null)
            {
                return NotFound();
            }

            return View(tProductosVendido);
        }

        // POST: ProductosVendidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TProductosVendidos == null)
            {
                return Problem("Entity set 'DB_FARMACIAContext.TProductosVendidos'  is null.");
            }
            var tProductosVendido = await _context.TProductosVendidos.FindAsync(id);
            if (tProductosVendido != null)
            {
                _context.TProductosVendidos.Remove(tProductosVendido);
            }
            
            await _context.SaveChangesAsync();
            TempData["error"] = "El produto ha sido eliminado";
            return RedirectToAction(nameof(Index));
        }

        private bool TProductosVendidoExists(int id)
        {
          return (_context.TProductosVendidos?.Any(e => e.CodigoProducto == id)).GetValueOrDefault();
        }
    }
}
