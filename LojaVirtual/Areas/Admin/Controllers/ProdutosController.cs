﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaVirtual.Data;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace LojaVirtual.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;

        private IWebHostEnvironment _he; // Antes estava tentando usando o IHostingEnvironment que é OBSOLETO

        [Obsolete]
        public ProdutosController(ApplicationDbContext context, IWebHostEnvironment he) // Antes estava tentando usando o IHostingEnvironment que é OBSOLETO
        {
            _context = context;
            _he = he;
        }

        // GET: Admin/Produtos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Produtos.Include(p => p.Categorias).Include(p => p.TagEspecial);
            return View(await applicationDbContext.ToListAsync());
        }

        // POST: Index
        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var produtos = _context.Produtos.Include(c => c.Categorias).Include(c => c.TagEspecial)
                .Where(c => c.Preco >= lowAmount && c.Preco <= largeAmount).ToList();
            if (lowAmount == null || largeAmount == null)
            {
                produtos = _context.Produtos.Include(c => c.Categorias).Include(c => c.TagEspecial).ToList();
            }
            return View(produtos);
        }


        // GET: Admin/Produtos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Categoria");
            ViewData["TagEspecialId"] = new SelectList(_context.TagEspecial, "Id", "Name");

            return View();
        }

        // POST: Admin/Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Preco,Imagem,ProdutoCor,Disponibilidade,CategoriaId,TagEspecialId")] Produtos produtos,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                //METODO QUE VERIFICA SE PRODUTO JÁ EXISTE ANTES DE CRIAR
                var searchProduct = _context.Produtos.FirstOrDefault(c => c.Nome == produtos.Nome);
                if (searchProduct != null)
                {
                    ViewBag.message = "Este produto já existe";
                    ViewData["CategoriaId"] = new SelectList(_context.Categorias.ToList(), "Id", "Categoria");
                    ViewData["TagEspecialId"] = new SelectList(_context.TagEspecial.ToList(), "Id", "Name");
                    
                    return View(produtos);
                }


                //METODO DE CAMINHO PARA A IMAGEM E CASO NULL EXIBE SEM IMAGEM
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    produtos.Imagem = "Images/" + image.FileName;
                }
                if (image == null)
                {
                    produtos.Imagem = "Images/noimage.jpg";
                }
                _context.Add(produtos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Categoria", produtos.CategoriaId); //ADD nos parametros como deseja exibir
            ViewData["TagEspecialId"] = new SelectList(_context.TagEspecial, "Id", "Name", produtos.TagEspecialId); //ADD nos parametros como deseja exibir
            return View(produtos);
        }

        // GET: Admin/Produtos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = _context.Produtos.Include(c => c.Categorias).Include(c => c.TagEspecial).FirstOrDefault(c => c.Id == id);
            if (produtos == null)
            {
                return NotFound();
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Categoria", produtos.CategoriaId); //ADD nos parametros como deseja exibir
            ViewData["TagEspecialId"] = new SelectList(_context.TagEspecial, "Id", "Name", produtos.TagEspecialId); //ADD nos parametros como deseja exibir

            return View(produtos);
        }

        // POST: Admin/Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,Imagem,ProdutoCor,Disponibilidade,CategoriaId,TagEspecialId")] Produtos produtos,IFormFile image)
        {
            if (id != produtos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //METODO DE CAMINHO PARA A IMAGEM E CASO NULL EXIBE SEM IMAGEM
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    produtos.Imagem = "Images/" + image.FileName;
                }
                if (image == null)
                {
                    produtos.Imagem = "Images/noimage.jpg";
                }
                _context.Produtos.Update(produtos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Categoria", produtos.CategoriaId); //ADD nos parametros como deseja exibir
            ViewData["TagEspecialId"] = new SelectList(_context.TagEspecial, "Id", "Name", produtos.TagEspecialId); //ADD nos parametros como deseja exibir

            return View(produtos);
        }

        // GET: Admin/Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos
                .Include(p => p.Categorias)
                .Include(p => p.TagEspecial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        // GET: Admin/Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos
                .Include(p => p.Categorias)
                .Include(p => p.TagEspecial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        // POST: Admin/Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtos = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produtos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutosExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
