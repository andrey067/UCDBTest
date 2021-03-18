using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                                   (x =>
                                    x.Nome_produto.ToLower().Contains(nome_produto.ToLower()))
                                    .AsNoTracking()
                                    .ToListAsync();

            return produto.FirstOrDefault();
        }

        public async Task<List<Produto>> SearchByNome(string nome_produto)
        {
            var AllProdutos = await _context.Produtos.Where(x => x.Nome_produto.ToLower()
                .Contains(nome_produto.ToLower()))
                .AsNoTracking().ToListAsync();

            return AllProdutos;

        }


        public async Task<List<Produto>> SearchByData_vencimento(DateTime data_vencimento)
        {
            var datasProdutos = await _context.Produtos.Where(x => x.Data_vencimento == data_vencimento).AsTracking().ToListAsync();

            return datasProdutos;

        }

        

        //TODO
        public Task<Produto> SearchByValor(decimal valor)
        {
            throw new NotImplementedException();
        }
    }


}