using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PWEB.Models
{
    public class Constant
    {

        public enum Tipo_User
        {
            [Display(Name = "Funcionario")]
            FUNCIONARIO,

            [Display(Name = "Particular")]
            PARTICULAR
        }


        public enum Tipo_Veiculo
        {
            [Display(Name = "Comercial")]
            COMERCIAL,

            [Display(Name = "SUV")]
            SUV,

            [Display(Name = "PickUp")]
            PICKUP,

            [Display(Name = "Desportivo")]
            DESPORTIVO,

            [Display(Name = "Urbano")]
            URBANO
        }


    }
}