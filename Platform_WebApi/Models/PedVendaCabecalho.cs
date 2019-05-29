using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Platform_WebApi.Models
{
    public class PedVendaCabecalho
    {
        public int idPedCabecalho { get; set; }
        public int idCliente { get; set; }
        public DateTime dtPedido { get; set; }
        public DateTime dtEntrega { get; set; }

        [Display(Name = "Desconto")]
        [Required(ErrorMessage = "Informe o desconto")]
        public decimal desconto { get; set; }

        [Display(Name = "Valor Total")]
        [Required(ErrorMessage = "Informe o valor total")]
        public decimal valTotal { get; set; }
    }
}