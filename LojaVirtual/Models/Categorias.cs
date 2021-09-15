using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Categorias
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; }


    }
}
