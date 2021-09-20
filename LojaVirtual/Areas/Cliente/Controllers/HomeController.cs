using LojaVirtual.Data;
using LojaVirtual.Models;
using LojaVirtual.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace LojaVirtual.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            return View(_context.Produtos.Include(c => c.Categorias).Include(c => c.TagEspecial).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //GET Detalhes de produtos método action
        public ActionResult Detail(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var produto = _context.Produtos.Include(c => c.Categorias).FirstOrDefault(c => c.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        //POST Detalhes de produtos método action
        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProdutoDetalhes(int? id)
        {
            List<Produtos> produtos = new List<Produtos>();
            if (id == null)
            {
                return NotFound();
            }

            var produto = _context.Produtos.Include(c => c.Categorias).FirstOrDefault(c => c.Id == id);
            if (produtos == null)
            {
                return NotFound();
            }

            produtos = HttpContext.Session.Get<List<Produtos>>("produtos");
            if (produtos == null)
            {
                produtos = new List<Produtos>();
            }
            produtos.Add(produto);
            HttpContext.Session.Set("produtos", produtos);
            return RedirectToAction(nameof(Index));
        }

        //GET Remover método action
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<Produtos> produtos = HttpContext.Session.Get<List<Produtos>>("produtos");
            if (produtos != null)
            {
                var produto = produtos.FirstOrDefault(c => c.Id == id);
                if (produto != null)
                {
                    produtos.Remove(produto);
                    HttpContext.Session.Set("produtos", produtos);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Produtos> produtos = HttpContext.Session.Get<List<Produtos>>("produtos");
            if (produtos != null)
            {
                var produto = produtos.FirstOrDefault(c => c.Id == id);
                if (produto != null)
                {
                    produtos.Remove(produto);
                    HttpContext.Session.Set("produtos", produtos);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //GET Carro de compras método action
        public IActionResult Cart()
        {
            List<Produtos> produtos = HttpContext.Session.Get<List<Produtos>>("produtos");
            if(produtos == null)
            {
                produtos = new List<Produtos>();
            }
            return View(produtos);
        }


    }
}
