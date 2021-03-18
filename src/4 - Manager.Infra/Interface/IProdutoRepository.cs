using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Infra.Interface
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Task<Produto> GetByNome_produto(string nome_produto);

        Task<List<Produto>> SearchByNome(string nome_produto);

        Task<List<Produto>> SearchByData_vencimento(DateTime data_vencimento);

        Task<Produto> SearchByValor(decimal valor);




    }


}
