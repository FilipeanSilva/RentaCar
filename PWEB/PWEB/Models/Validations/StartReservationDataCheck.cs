using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PWEB.Models.Validations
{
    public class StartReservationDataCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var reservation = (Reserva)validationContext.ObjectInstance;

            if (reservation.Datar_i > DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Start Time needs to be greater than the current time!");
        }
    }
}