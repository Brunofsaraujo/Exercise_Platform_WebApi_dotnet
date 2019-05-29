using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Platform_WebApi.Models
{
    public class Item
    {
        public int idItem { get; set; }

        [Display(Name = "Descricao")]
        [Required(ErrorMessage = "Informe a descricao do produto", AllowEmptyStrings = false)]
        public string descricao { get; set; }

        [Display(Name = "Valor Unitario")]
        [Required(ErrorMessage = "Informe o valor do produto")]
        public decimal valorUnit { get; set; }

        [Display(Name = "Qtde Estoque")]
        [Required(ErrorMessage = "Informe a quantidade do produto em estoque")]
        public int qtdeEstoque { get; set; }
    }
}