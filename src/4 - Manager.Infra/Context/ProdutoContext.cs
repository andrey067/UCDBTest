using System;
using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Context
{
    public class ProdutoContext : DbContext
    {
        public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options)
        {

        }

        public virtual DbSet<Produto> Produtos { get; set; }


    }

}
