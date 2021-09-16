using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace LojaVirtual.Models
{
    public class Produtos
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
        public string Imagem { get; set; }

        [Display(Name = "Cor")]
        public string ProdutoCor { get; set; }
        [Required]
        [Display(Name = "Disponibilidade")]        // Verificar a opção de trocar para ENUM - selecinando Disponível = 0 e Indisponível = 1
        public bool Disponibilidade { get; set; }

        [Display(Name = "Categoria")]
        [Required]
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categorias Categorias { get; set; }
        [Display(Name = "Tag")]
        [Required]
        public int TagEspecialId { get; set; }
        [ForeignKey("TagEspecialId")]
        public TagEspecial TagEspecial { get; set; }

    }
}
