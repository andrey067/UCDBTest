using Manager.Services.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Services.Interface
{
    public interface IProdutoService
    {
        Task<ProdutoDTO> Create(ProdutoDTO produtoDTO);
        Task<ProdutoDTO> Update(ProdutoDTO produtoDTO);
        Task Remove(long id);
        Task<ProdutoDTO> Get(long id);
        Task<List<ProdutoDTO>> GetAll();

        Task<List<ProdutoDTO>> SearchByNome_Produto(string nome_produto);

        Task<List<ProdutoDTO>> SearchByData_vencimento(DateTime data_vencimento);

    }
}
