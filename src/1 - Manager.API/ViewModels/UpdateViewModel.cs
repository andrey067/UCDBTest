using System;
using System.ComponentModel.DataAnnotations;


namespace Manager.API.ViewModels
{
    public class UpdateViewModel
    {
        [Required(ErrorMessage ="Id não pode ser vazio")]
        [Range(1,int.MaxValue, ErrorMessage ="Id não pode ser menor que 1")]
        public int Id { get; set; }

        [Display(Name = "Nome do produto")]
        [Required(ErrorMessage = "O nome não pode ser nulo")]
        [MinLength(3, ErrorMessage = "Deve ter no minimo 3 caracteres")]
        [MaxLength(80, ErrorMessage = "Deve ter o maximo 80 caracteres")]
        public string Nome_produto { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "O valor não pode ser nulo")]
        //[Range(18,2 ,ErrorMessage = "Valor dever estar dentro desta varição (18,4)")]
        public decimal Valor { get; set; }


        [Display(Name = "Data de vencimento")]
        [Required(ErrorMessage = "O data não pode ser nula")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime Data_vencimento { get; set; }




    }
}
