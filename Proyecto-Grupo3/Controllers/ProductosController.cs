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
    [Authorize(Roles = "Administrador")]
    public class ProductosController : Controller
    {
        private readonly DB_FARMACIAContext _context;

        public ProductosController(DB_FARMACIAContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var dB_FARMACIAContext = _context.TProductos.Include(t => t.CodigoTipoProductoNavigation);
            return View(await dB_FARMACIAContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.TProductos == null)
            {
                return NotFound();
            }

            var tProducto = await _context.TProductos
                .Include(t => t.CodigoTipoProductoNavigation)
                .FirstOrDefaultAsync(m => m.NombreCategoria == id);
            if (tProducto == null)
            {
                return NotFound();
            }

            return View(tProducto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["CodigoTipoProducto"] = new SelectList(_context.TTiposProductos, "CodigoTipoProducto", "CodigoTipoProducto");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreCategoria,CodigoTipoProducto,NombreProducto")] TProducto tProducto)
        {
            try 
            {
                _context.Add(tProducto);
                await _context.SaveChangesAsync();
                TempData["success"] = "El producto ha sido creado";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw; 
            }
            ViewData["CodigoTipoProducto"] = new SelectList(_context.TTiposProductos, "CodigoTipoProducto", "CodigoTipoProducto", tProducto.CodigoTipoProducto);
            return View(tProducto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.TProductos == null)
            {
                return NotFound();
            }

            var tProducto = await _context.TProductos.FindAsync(id);
            if (tProducto == null)
            {
                return NotFound();
            }
            ViewData["CodigoTipoProducto"] = new SelectList(_context.TTiposProductos, "CodigoTipoProducto", "CodigoTipoProducto", tProducto.CodigoTipoProducto);
            return View(tProducto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NombreCategoria,CodigoTipoProducto,NombreProducto")] TProducto tProducto)
        {
            if (id != tProducto.NombreCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tProducto);
                    await _context.SaveChangesAsync();
                    TempData["edit"] = "El producto ha sido editado";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TProductoExists(tProducto.NombreCategoria))
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
            ViewData["CodigoTipoProducto"] = new SelectList(_context.TTiposProductos, "CodigoTipoProducto", "CodigoTipoProducto", tProducto.CodigoTipoProducto);
            return View(tProducto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.TProductos == null)
            {
                return NotFound();
            }

            var tProducto = await _context.TProductos
                .Include(t => t.CodigoTipoProductoNavigation)
                .FirstOrDefaultAsync(m => m.NombreCategoria == id);
            if (tProducto == null)
            {
                return NotFound();
            }

            return View(tProducto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TProductos == null)
            {
                return Problem("Entity set 'DB_FARMACIAContext.TProductos'  is null.");
            }
            var tProducto = await _context.TProductos.FindAsync(id);
            if (tProducto != null)
            {
                _context.TProductos.Remove(tProducto);
            }
            
            await _context.SaveChangesAsync();
            TempData["error"] = "El producto ha sido eliminado";
            return RedirectToAction(nameof(Index));
        }

        private bool TProductoExists(string id)
        {
          return (_context.TProductos?.Any(e => e.NombreCategoria.ToString() == id)).GetValueOrDefault();
        }
    }
}
