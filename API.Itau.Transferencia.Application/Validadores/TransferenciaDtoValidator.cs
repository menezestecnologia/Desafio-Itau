using API.Itau.Transferencia.Domain.DTOs;
using FluentValidation;

namespace API.Itau.Transferencia.Application.Validadores
{
    public class TransferenciaDtoValidator : AbstractValidator<TransferenciaDto>
    {
        public TransferenciaDtoValidator()
        {
            RuleFor(x => x.ContaOrigem)
                .NotEmpty().WithMessage("Conta de origem é obrigatória");

            RuleFor(x => x.ContaDestino)
                .NotEmpty().WithMessage("Conta de destino é obrigatória")
                .NotEqual(x => x.ContaOrigem).WithMessage("Conta de destino deve ser diferente da origem");

            RuleFor(x => x.Valor)
                .GreaterThan(0).WithMessage("Valor da transferência deve ser maior que zero")
                .LessThanOrEqualTo(10000).WithMessage("Valor máximo permitido é R$ 10.000");
        }
    }
}