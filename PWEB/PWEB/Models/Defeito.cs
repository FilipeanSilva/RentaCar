using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PWEB.Models
{
    public class Defeito
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Id Reserva")]
        public virtual int Id_reserva { get; set; }

        [ForeignKey("Id_reserva")]
        public virtual Reserva Reservas { get; set; }

        [MaxLength(500)]
        public string DanosExteriores { get; set; }

        [MaxLength(500)]
        public string DanosInteriores { get; set; }
    }
}