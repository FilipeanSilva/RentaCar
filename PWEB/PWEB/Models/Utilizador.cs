using PWEB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace PWEB.Models
{
    public class Utilizador
    {
        [Key]
        public int Utilizador_Id { get; set; }

        [Required(ErrorMessage = "Obrigatório declarar o Nome! ")]
        [DataType(DataType.Text), MaxLength(80)]
        public string Name { get; set; }



        //para aceder à tabela Conta
        [ForeignKey("Conta")]
        [Required]
        public int ContaId { get; set; }
        public Conta Conta { get; set; }

    }
}