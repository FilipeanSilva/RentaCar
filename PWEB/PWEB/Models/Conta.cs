using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PWEB.Models
{
    public class Conta
    {
        public int ContaId { get; set; }

        [Required]
        [DataType(DataType.Text), MaxLength(100)]
        public string Nome_Conta { get; set; }

        [Required(ErrorMessage = "Obrigatório inserir número de conta! ")]
        public int Numero_Conta { get; set; }



    }
}