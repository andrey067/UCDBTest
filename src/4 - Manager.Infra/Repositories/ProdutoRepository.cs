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

        public async Task<List<Produto>> SearchByNome(string nome_produto)
        {
            var AllProdutos = await _context.Produtos.Where(x => x.Nome_produto.ToLower()
                .Contains(nome_produto.ToLower()))
                .AsNoTracking().ToListAsync();

            return AllProdutos;

        }

        public async Task<List<Produto>> SearchByValor(decimal valor)
        {
            var AllPRodutos = await _context.Produtos.Where(x => x.Valor == valor)
                .AsNoTracking()
                .ToListAsync();

            return AllPRodutos;

        }


        public async Task<List<Produto>> SearchByData_vencimento(DateTime data_vencimento)
        {
            var datasProdutos = await _context.Produtos.Where(x => x.Data_vencimento == data_vencimento).AsTracking().ToListAsync();

            return datasProdutos;

        }

    }


}