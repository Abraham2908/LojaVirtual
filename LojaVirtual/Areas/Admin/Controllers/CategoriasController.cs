using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaVirtual.Data;
using LojaVirtual.Models;

namespace LojaVirtual.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Categorias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        // GET: Admin/Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categorias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Categoria")] Categorias categorias)
        {
            if (ModelState.IsValid)
            {
                _context.Categorias.Add(categorias);
                await _context.SaveChangesAsync();
                TempData["save"] = "A categoria foi criada com sucesso";
                return RedirectToAction(nameof(Index));
            }
            return View(categorias);
        }

        // GET: Admin/Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorias = await _context.Categorias.FindAsync(id);
            if (categorias == null)
            {
                return NotFound();
            }
            return View(categorias);
        }

        // POST: Admin/Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Categoria")] Categorias categorias)
        {
            if (id != categorias.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categorias);
                    await _context.SaveChangesAsync();
                    TempData["edit"] = "A categoria foi alterada com sucesso";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriasExists(categorias.Id))
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
            return View(categorias);
        }


        // GET: Admin/Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorias = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categorias == null)
            {
                return NotFound();
            }

            return View(categorias);
        }

        //POST: Admin/Categorias/Details/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Categorias categorias)
        {
            return RedirectToAction(nameof(Index));

        }


        // GET: Admin/ProdutoTipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorias = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categorias == null)
            {
                return NotFound();
            }

            return View(categorias);
        }

        // POST: Admin/ProdutoTipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categorias = await _context.Categorias.FindAsync(id);
            _context.Categorias.Remove(categorias);
            await _context.SaveChangesAsync();
            TempData["delete"] = "A categoria foi excluída.";
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriasExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }

}

//Inicio Criado na Aula
//[Area("Admin")]
//public class ProdutoTiposController : Controller
//{
//    private ApplicationDbContext _db;

//    public ProdutoTiposController(ApplicationDbContext db)
//    {
//        _db = db;
//    }
//    public IActionResult Index()
//    {
//        //var data = _db.ProdutoTipos.ToList();
//        return View(_db.ProdutoTipos.ToList());
//    }
//}