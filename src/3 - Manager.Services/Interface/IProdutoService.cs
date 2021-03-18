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
        Task<ProdutoDTO> Get(ProdutoDTO produtoDTO);
        Task<List<ProdutoDTO>> GetAll(ProdutoDTO produtoDTO);

    }
}
