using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using Manager.Core.Exceptions;
using Manager.Domain.Validators;

namespace Manager.Domain.Entities
{
    public class Produto : Base
    {

        // Private Set, entidade fechada, somente podendo ser mudando por contrutor da propria classe
        //Propriedades
        public string Nome_produto { get; private set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Valor { get; private set; }

        public DateTime Data_vencimento { get; private set; }


        //EF -Construtor para o Entity Framework
        protected Produto() { }



        //Construtor padrão
        public Produto(string nome_produto, decimal valor, DateTime data_vencimento)
        {
            Nome_produto = nome_produto;
            Valor = valor;
            Data_vencimento = data_vencimento;
            _errors = new List<string>();

            Validate();
        }

        //Comportamento da entidade
        public void ChageName_Produto(string nome_produto)
        {
            Nome_produto = nome_produto;
            Validate();
        }

        public void ChangeValor(decimal valor)
        {
            Valor = valor;
            Validate();
        }


        public void ChangeData_vencimento(DateTime data_vencimento)
        {
            Data_vencimento = data_vencimento;
            Validate();
        }

        //Validação da entidade
        public override bool Validate()
        {
            var validator = new ProdutoValidator();
            var validation =  validator.Validate(this);

            //Pega os erros na camada de dominio
            if (!validation.IsValid) 
            {
                if (!validation.IsValid)
                {
                    foreach (var error in validation.Errors)
                        _errors.Add(error.ErrorMessage);

                    throw new DomainException("Alguns campos estão inválidos, por favor corrija-os!", _errors);
                }
            }
             return true;
        }

    }



}
