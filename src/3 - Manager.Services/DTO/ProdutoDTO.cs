using System;


namespace Manager.Services.DTO
{
    public class ProdutoDTO
    {
        public long Id { get; set; }

        public string Nome_produto { get;  set; }

        public decimal Valor { get;  set; }

        public DateTime Data_vencimento { get;  set; }



        public ProdutoDTO()
        {

        }

        public ProdutoDTO(long id, string nome_produto, decimal valor, DateTime data_vencimento)
        {
            Id = id;
            Nome_produto = nome_produto;
            Valor = valor;
            Data_vencimento = data_vencimento;
        }



    }
}
