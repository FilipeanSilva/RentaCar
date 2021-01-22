using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PWEB.Models.Validations
{
    public class EndReservationDataCheck : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var reservation = (Reserva)validationContext.ObjectInstance;

            if (reservation.Datar_f > reservation.Datar_i)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("End Time needs to be bigger than Start Time!");
        }

    }
}