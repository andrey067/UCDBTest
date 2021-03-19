using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class ProdutoView
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nome_produto")]
        public string Nome_produto { get; set; }

        [JsonProperty("valor")]
        public decimal Valor { get; set; }

        [JsonProperty("data_vencimento")]
        public DateTime Data_vencimento { get; set; }



        public ProdutoView()
        {

        }

        public ProdutoView(long id, string nome_produto, decimal valor, DateTime data_vencimento)
        {
            Id = id;
            Nome_produto = nome_produto;
            Valor = valor;
            Data_vencimento = data_vencimento;
        }


    }
}
