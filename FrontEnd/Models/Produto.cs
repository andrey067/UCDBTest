using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class Produto
    {


        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("nome_produto")]
        [Required(ErrorMessage = "Campo nome é obrigatorio")]
        [Display(Name = " Nome do produto")]
        public string nome_produto { get; set; }

        [JsonProperty("valor")]
        [Required(ErrorMessage = "Campo valor é obrigatorio")]
        [Display(Name = " Valor do produto")]
        [DataType(DataType.Currency)]
        public double valor { get; set; }

        [JsonProperty("data_vencimento")]
        [Required(ErrorMessage = "Campo data de vencimento é obrigatorio")]
        [Display(Name = " Data de  validade")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime data_vencimento { get; set; }

        [JsonProperty("color")]
        [Display(Name = "Status")]
        public string? color { get; set; }

        public Produto(int id, string nome_produto, double valor, DateTime data_vencimento, string color)
        {
            this.id = id;
            this.nome_produto = nome_produto;
            this.valor = valor;
            this.data_vencimento = data_vencimento;
            this.color = color;
        }
        public Produto() { }

        public Produto(string nome_produto, double valor, DateTime data_vencimento)
        {
            this.nome_produto = nome_produto;
            this.valor = valor;
            this.data_vencimento = data_vencimento;
        }



    }
}
