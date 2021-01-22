using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PWEB.Models.Validations
{
    public class VeiculosUsage : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var reservation = (Reserva)validationContext.ObjectInstance;

            var db = new ApplicationDbContext();

            var veiculo = db.Veiculos.Find(reservation.Id_veiculo);

            if (veiculo == null)
            {
                //TODO: Necessary?
                return new ValidationResult("Necessário Introduzir um Veiculo");
            }

            var OverlapedReservationsWithSelectedVeiculo = db.Reservas
                .Where(r => r.Id_veiculo == veiculo.Id_veiculo)
                .Where(r => r.Id_reserva != reservation.Id_reserva)
                .Where(r => r.Datar_i < reservation.Datar_f && r.Datar_f > reservation.Datar_i)
                .ToList();


            if (OverlapedReservationsWithSelectedVeiculo.Count == 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Já existe uma reserva para esse período de Tempo");
        }
    }
}