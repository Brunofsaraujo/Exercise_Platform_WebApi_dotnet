using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Platform_WebApi.Models
{
    public class PedVendaLinhas
    {
        public int idPedLinhas { get; set; }
        public int idPedCabecalho { get; set; }
        public int idItem { get; set; }

        [Display(Name = "Valor Unitario")]
        [Required(ErrorMessage = "Informe o valor do produto")]
        public decimal valUnit { get; set; }

        [Display(Name = "Qtde Pedido")]
        [Required(ErrorMessage = "Informe a quantidade")]
        public int pedQtde { get; set; }
    }
}