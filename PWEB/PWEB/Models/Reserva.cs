using PWEB.Models.Validations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PWEB.Models
{
    public class Reserva
    {
       [Key]
        public int Id_reserva { get; set; }
        [StartReservationDataCheck]
        [Required(ErrorMessage = "Obrigatório colocar Data de Reserva Inicial!")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Data Inicial de Reserva")]
        [DataType(DataType.Date)]
        public DateTime Datar_i { get; set; }

        [EndReservationDataCheck]
        [Required(ErrorMessage = "Obrigatório colocar Data de Reserva Final!")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Data Final de Reserva")]
        [DataType(DataType.Date)]
        public DateTime Datar_f { get; set; }

        [Display(Name = "Preço Total")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double PrecoTotal { get; set; }

        public bool Disponivel { get; set; } // disponivel ou nao para reserva

        [Display(Name = "Veiculo")]
        [VeiculosUsage]
        public virtual int Id_veiculo { get; set; }

        [ForeignKey("Id_veiculo")]
        public virtual Veiculo Veiculos { get; set; }

        [Display(Name = "Utilizador")]
        public virtual string Id_user { get; set; }

        [ForeignKey("Id_user")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public double updatePrecoTotal(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var reservaidve = db.Reservas.Where(a => a.Id_veiculo == id)
                .Select(a=> a.Id_veiculo).FirstOrDefault();
            var valor = db.Veiculos.Where(a => a.Id_veiculo == reservaidve)
                .Select(a => a.PrecoDia).FirstOrDefault();
            
            if (Veiculos != null)
            {
                TimeSpan timeSpan = Datar_f - Datar_i;
                int totalDias = (int)timeSpan.TotalDays;

                PrecoTotal = Veiculos.PrecoDia * totalDias;
                return PrecoTotal;
            }
            else
            {
                TimeSpan timeSpan = Datar_f - Datar_i;
                int totalDias = (int)timeSpan.TotalDays;

                PrecoTotal = valor * totalDias;
                //PrecoTotal = 0;
                return PrecoTotal;
            }
        }
    }
}