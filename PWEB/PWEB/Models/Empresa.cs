using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PWEB.Models
{
    public class Empresa
    {

        [Key]
        public int Id_empresa { get; set; }

        public string Nome_Empresa { get; set; }
    }
}