using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Infra.Repositories
{

	public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
	{
        private readonly ProdutoContext _context;

        public ProdutoRepository(ProdutoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Produto> GetByNome_produto(string nome_produto)
        {
            var produto = await _context.Produtos.Where
                                   (
                                        x =>
                                            x.Nome_produto.ToLower() == nome_produto.ToLower()
                                    )
                                    .AsNoTracking()
                                    .ToListAsync();

            return produto.FirstOrDefault();
        }

        public Task<Produto> SearchByData_vencimento(DateTime data_vencimento)
        {
            throw new NotImplementedException();
        }

        public Task<Produto> SearchByNome(string nome_produto)
        {
            throw new NotImplementedException();
        }

        public Task<Produto> SearchByValor(decimal valor)
        {
            throw new NotImplementedException();
        }
    }


}