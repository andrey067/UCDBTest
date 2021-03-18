using System;
using Manager.DomainException.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Context
{
    public class ProdutoContext : DbContext
    {
        public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options)
        {

        }
        //Criação da tebela pelo EF - para a StartUp.cs, encontrar o Manager.Infra
        //Fazendo a referencia -> Add-Migration NewMigration -Project Manager
        public virtual DbSet<Produto> Produtos { get; set; }


    }

}
