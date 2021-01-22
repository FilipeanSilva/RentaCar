using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PWEB.Models
{
    public class Categoria
    {
        [Key]
        public int Id_categoria { get; set; }

        
        [Required(ErrorMessage = "Obrigatório colocar nome da Categoria!")] // como será o alerta passado para o user
        [StringLength(16, ErrorMessage = "Obrigatório ter entre 3 e 16 caracteres!", MinimumLength = 3)]
        public string NomeCategoria { get; set; }

        [Display(Name = "Descrição")]
        [DataType(DataType.Text), MaxLength(120)]
        public string Descricao { get; set; }

        [Required]
        public bool Estado { get; set; } // ativado ou desativado
    }
}