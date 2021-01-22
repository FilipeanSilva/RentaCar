using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PWEB.Models
{
    public class Entrega
    {
        [Key]
        public int Id_Entrega { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Data_Entregai { get; set; } //verdadeira data de quando o veiculo foi entregue

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Data_Entregaf { get; set; } //verdadeira data de quando o veiculo foi entregue

        //fk do veiculo
        [Display(Name = "Reserva")]
        public virtual int Id_reserva { get; set; }

        [ForeignKey("Id_reserva")]
        public virtual Reserva Reservas { get; set; }

        [Display(Name = "UserName")]
        public string NomeUser { get; set; }

        [Display(Name = "Entregue")]
        public bool IsEntregue { get; set; }

        [Display(Name = "Recebido")]
        public bool IsRecebido { get; set; }
    }
}