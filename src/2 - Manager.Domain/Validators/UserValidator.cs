using System;
using FluentValidation;
using Manager.Domain.Entities;



namespace Manager.Domain.Validators
{
    public class UserValidator : AbstractValidator<Produto>
    {
        public UserValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade não pode ser vazia.")

                .NotNull()
                .WithMessage("A entidade não pode ser nula.");

            RuleFor(x => x.Nome_produto)
                .NotNull()
                .WithMessage("O nome não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O nome não pode ser vazio.")

                .MinimumLength(3)
                .WithMessage("O nome do produto deve ter no mínimo 3 caracteres.")

                .MaximumLength(80)
                .WithMessage("O nome do produto deve ter no máximo 80 caracteres.");


            RuleFor(x => x.Valor)
                .NotNull()
                .WithMessage("O valor não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O nome não pode ser vazio.")

                .GreaterThan(0.0M)
                .WithMessage("O valor deve ser maior que zero");


            RuleFor(x => x.Data_vencimento)
                .NotNull()
                .WithMessage("O valor não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O nome não pode ser vazio.")
                
                // Regrade negocio não especificada
                .GreaterThan(DateTime.Now)
                .WithMessage("Data deve ser maior que a data de hoje");

        }





    }


}

