using System;
using System.Threading.Tasks;

namespace Manager.Infra.Interface
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Task<Produto> GetByNome_produto(string nome_produto);

        Task<Produto> SearchByNome(string nome_produto);

        Task<Produto> SearchByData_vencimento(DateTime data_vencimento);

        Task<Produto> SearchByValor(decimal valor);




    }


}
