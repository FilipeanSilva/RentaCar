using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PWEB.Models
{
    public class Veiculo
    {
        [Key]
        public int Id_veiculo { get; set; }

        [Required(ErrorMessage = "É necessário um nome!")]
        [StringLength(16, ErrorMessage = "Obrigatório ter entre 3 e 16 caracteres!", MinimumLength = 3)]
        public string Nome_veiculo { get; set; }

        [Required(ErrorMessage = "Obrigatório colocar a Marca!")]
        [DataType(DataType.Text), MaxLength(80)]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Obrigatório colocar a Cor!")]
        [DataType(DataType.Text), MaxLength(80)]
        [Display(Name = "Cor")]
        public string Cor { get; set; }

        [Required(ErrorMessage = "Obrigatório colocar a Quilometragem!")]
        [Display(Name = "Quilometros")]
        public int Kilometros { get; set; }

        [Required(ErrorMessage = "Obrigatório colocar a Quantidade de Defeitos!")]       
        [Display(Name = "Quantidade de Defeitos")]
        public int Defeitos { get; set; }

        //ID_Categoria -> FK
        [Display(Name = "Categoria")]
        public virtual int Id_categoria { get; set; }

        [ForeignKey("Id_categoria")]
        public virtual Categoria Categorias { get; set; }

        [MaxLength(500)]
        public string Imagem { get; set; }

        [Display(Name = "Preço por Dia")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double PrecoDia { get; set; }

        //fk para empresa
        [Display(Name = "Empresa")]
        public virtual string Id { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public bool isDisponivel()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var veiculo = db.Reservas.FirstOrDefault(a => a.Id_veiculo == this.Id_veiculo);

            if (veiculo != null)
            {
                return false;
            }

            return true;
        }
    }
}