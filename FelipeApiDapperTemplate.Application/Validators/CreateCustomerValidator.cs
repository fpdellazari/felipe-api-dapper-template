using FelipeApiDapperTemplate.Domain.Models.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FelipeApiDapperTemplate.Application.Validators;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerDTO>
{
    public CreateCustomerValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 caracteres.");

        RuleFor(customer => customer.Age)
            .InclusiveBetween(18, 120).WithMessage("A idade deve ser entre 18 e 120 anos.");

        RuleFor(customer => customer.Phone)
            .NotEmpty().WithMessage("O telefone é obrigatório.")
            .Matches(new Regex(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[0-9])[0-9]{3}\-?[0-9]{4}$")).WithMessage("O telefone não é válido.");

        RuleFor(customer => customer.Email)
            .EmailAddress().WithMessage("O e-mail não é válido");
    }
}
