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
    public class TiposProductosController : Controller
    {
        private readonly DB_FARMACIAContext _context;

        public TiposProductosController(DB_FARMACIAContext context)
        {
            _context = context;
        }

        // GET: TiposProductos
        public async Task<IActionResult> Index()
        {
              return _context.TTiposProductos != null ? 
                          View(await _context.TTiposProductos.ToListAsync()) :
                          Problem("Entity set 'DB_FARMACIAContext.TTiposProductos'  is null.");
        }

        // GET: TiposProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TTiposProductos == null)
            {
                return NotFound();
            }

            var tTiposProducto = await _context.TTiposProductos
                .FirstOrDefaultAsync(m => m.CodigoTipoProducto == id);
            if (tTiposProducto == null)
            {
                return NotFound();
            }

            return View(tTiposProducto);
        }

        // GET: TiposProductos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoTipoProducto,DescripcionTipoProducto")] TTiposProducto tTiposProducto)
        {
            if (ModelState.IsValid)
            {

                //CREAREMOS UNA VALIDACION PARA QUE NO HAYA REPETICION DE UN CODIGO DE PRODUCTO
                if (await _context.TTiposProductos.AnyAsync(i => i.CodigoTipoProducto == tTiposProducto.CodigoTipoProducto))
                {
                    ModelState.AddModelError("", "El Codigo del producto ingresado ya existe.");
                    return View(tTiposProducto);
                }

                if (await _context.TTiposProductos.AnyAsync(i => i.DescripcionTipoProducto == null))
                {
                    ModelState.AddModelError("", "Por favor ingrese la descripcion del producto.");
                    return View(tTiposProducto);
                }


                _context.Add(tTiposProducto);
                await _context.SaveChangesAsync();
                TempData["success"] = "El tipo de producto ha sido creado";
                return RedirectToAction(nameof(Index));
            }
            return View(tTiposProducto);
        }

        // GET: TiposProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TTiposProductos == null)
            {
                return NotFound();
            }

            var tTiposProducto = await _context.TTiposProductos.FindAsync(id);
            if (tTiposProducto == null)
            {
                return NotFound();
            }
            return View(tTiposProducto);
        }

        // POST: TiposProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoTipoProducto,DescripcionTipoProducto")] TTiposProducto tTiposProducto)
        {
            if (id != tTiposProducto.CodigoTipoProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tTiposProducto);
                    await _context.SaveChangesAsync();
                    TempData["edit"] = "El tipo de producto ha sido editado";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TTiposProductoExists(tTiposProducto.CodigoTipoProducto))
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
            return View(tTiposProducto);
        }

        // GET: TiposProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TTiposProductos == null)
            {
                return NotFound();
            }

            var tTiposProducto = await _context.TTiposProductos
                .FirstOrDefaultAsync(m => m.CodigoTipoProducto == id);
            if (tTiposProducto == null)
            {
                return NotFound();
            }

            return View(tTiposProducto);
        }

        // POST: TiposProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TTiposProductos == null)
            {
                return Problem("Entity set 'DB_FARMACIAContext.TTiposProductos'  is null.");
            }
            var tTiposProducto = await _context.TTiposProductos.FindAsync(id);
            if (tTiposProducto != null)
            {
                _context.TTiposProductos.Remove(tTiposProducto);
            }
            
            await _context.SaveChangesAsync();
            TempData["error"] = "El tipo de producto ha sido eliminado";
            return RedirectToAction(nameof(Index));
        }

        private bool TTiposProductoExists(int id)
        {
          return (_context.TTiposProductos?.Any(e => e.CodigoTipoProducto == id)).GetValueOrDefault();
        }
    }
}
