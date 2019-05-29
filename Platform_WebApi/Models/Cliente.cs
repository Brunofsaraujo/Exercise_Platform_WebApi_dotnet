using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Platform_WebApi.Models
{
    public class Cliente
    {        
        public int idCliente { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome do cliente", AllowEmptyStrings = false)]
        public string nomeCliente { get; set; }

        [Required(ErrorMessage = "Informe a senha do cliente", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string senhaCliente { get; set; }

        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Informe um email válido.")]
        public string emailCliente { get; set; }

        public string ufCliente { get; set; }                
    }
}