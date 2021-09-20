using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        [Display(Name = "Pedido No")]
        public string PedidoNo { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Telefone")]
        public string FoneNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Display(Name = "Data")]
        public DateTime PedidoData { get; set; }

        public virtual List<PedidoDetalhes> PedidoDetalhes { get; set; }
    }
}
