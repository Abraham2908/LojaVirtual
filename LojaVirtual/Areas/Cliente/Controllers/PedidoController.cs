using LojaVirtual.Data;
using LojaVirtual.Models;
using LojaVirtual.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class PedidoController : Controller
    {
        private ApplicationDbContext _context;

        public PedidoController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET Checkout action método

        public IActionResult Checkout()
        {
            return View();
        }

        //POST Checkout action método

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Pedido anOrder)
        {
            List<Produtos> produtos = HttpContext.Session.Get<List<Produtos>>("produtos");
            if (produtos != null)
            {
                foreach (var produto in produtos)
                {
                    PedidoDetalhes pedidoDetalhes = new PedidoDetalhes();
                    pedidoDetalhes.ProdutoId = produto.Id;
                    anOrder.PedidoDetalhes.Add(pedidoDetalhes);
                }
            }

            anOrder.PedidoNo = GetOrderNo();
            _context.Pedidos.Add(anOrder);
            await _context.SaveChangesAsync();
            HttpContext.Session.Set("produtos", new List<Produtos>());
            return View();
        }


        public string GetOrderNo()
        {
            int rowCount = _context.Pedidos.ToList().Count() + 1;
            return rowCount.ToString("000");
        }
    }
}
