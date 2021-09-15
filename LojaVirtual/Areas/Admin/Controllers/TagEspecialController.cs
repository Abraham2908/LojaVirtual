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
    public class TagEspecialController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagEspecialController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TagEspecial
        public async Task<IActionResult> Index()
        {
            return View(await _context.TagEspecial.ToListAsync());
        }

        // GET: Admin/TagEspecial/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagEspecial = await _context.TagEspecial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tagEspecial == null)
            {
                return NotFound();
            }

            return View(tagEspecial);
        }

        //POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(TagEspecial tagEspecial)
        {
            return RedirectToAction(nameof(Index));

        }


        // GET: Admin/TagEspecial/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TagEspecial/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TagEspecial tagEspecial)
        {
            if (ModelState.IsValid)
            {
                _context.TagEspecial.Add(tagEspecial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tagEspecial);
        }

        // GET: Admin/TagEspecial/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagEspecial = await _context.TagEspecial.FindAsync(id);
            if (tagEspecial == null)
            {
                return NotFound();
            }
            return View(tagEspecial);
        }

        // POST: Admin/TagEspecial/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TagEspecial tagEspecial)
        {
            if (id != tagEspecial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tagEspecial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagEspecialExists(tagEspecial.Id))
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
            return View(tagEspecial);
        }

        // GET: Admin/TagEspecial/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagEspecial = await _context.TagEspecial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tagEspecial == null)
            {
                return NotFound();
            }

            return View(tagEspecial);
        }

        // POST: Admin/TagEspecial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tagEspecial = await _context.TagEspecial.FindAsync(id);
            _context.TagEspecial.Remove(tagEspecial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagEspecialExists(int id)
        {
            return _context.TagEspecial.Any(e => e.Id == id);
        }
    }
}
