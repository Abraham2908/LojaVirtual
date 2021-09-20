using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class PedidoDetalhes
    {
        public int Id { get; set; }
        [Display(Name = "Pedido")]
        public int PedidoId { get; set; }
        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }

        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; }
        [ForeignKey("ProdutoId")]
        public Produtos Produto { get; set; }
    }
}
