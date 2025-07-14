using API.Itau.Transferencia.Domain.DTOs;
using FluentValidation;

namespace API.Itau.Transferencia.Application.Validadores
{
    public class ClienteDtoValidator : AbstractValidator<ClienteDto>
    {
        public ClienteDtoValidator()
        {
            RuleFor(x => x.Nome)
                 .NotEmpty().WithMessage("Nome é obrigatório")
                 .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.NumeroConta)
                .NotEmpty().WithMessage("Número da conta é obrigatório")
                .Matches(@"^\d{3,10}$").WithMessage("Número da conta deve conter apenas números (mín. 3 dígitos)");

            RuleFor(x => x.Saldo)
                .GreaterThanOrEqualTo(0).WithMessage("Saldo inicial não pode ser negativo");
        }
    }
}