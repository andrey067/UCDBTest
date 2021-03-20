using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class Produto
    {
        public Produto(int id, string nome_produto, double valor, DateTime data_vencimento, string color)
        {
            this.id = id;
            this.nome_produto = nome_produto;
            this.valor = valor;
            this.data_vencimento = data_vencimento;
            this.color = color;
        }

        public int id { get; set; }
        public string nome_produto { get; set; }
        public double valor { get; set; }
        public DateTime data_vencimento { get; set; }
        public string? color { get; set; }








    }
}
