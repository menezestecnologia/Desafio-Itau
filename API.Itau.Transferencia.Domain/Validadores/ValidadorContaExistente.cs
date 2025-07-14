using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Entidades;
using API.Itau.Transferencia.Domain.Interfaces.Services;

namespace API.Itau.Transferencia.Domain.Validadores
{
    public class ValidadorContaExistente(IClienteService clienteService) : IValidadorTransferencia
    {
        public readonly IClienteService _clienteService = clienteService;

        public async Task<string?> Validar(TransferenciaDto dto, Cliente? origem, Cliente? destino)
        {
            if (string.IsNullOrWhiteSpace(dto.ContaOrigem) && string.IsNullOrWhiteSpace(dto.ContaDestino))
                return "Contas de origem e destino não informadas";

            if (string.IsNullOrWhiteSpace(dto.ContaOrigem))
                return "Conta de origem não informada";

            if (string.IsNullOrWhiteSpace(dto.ContaDestino))
                return "Conta de destino não informada";

            var contaOrigemBusca = await _clienteService.ObterPorConta(dto.ContaOrigem);
            if (contaOrigemBusca == null)
                return "Conta de origem inexistente";

            var contaDestinoBusca = await _clienteService.ObterPorConta(dto.ContaDestino);
            if (contaDestinoBusca == null)
                return "Conta de destino inexistente";

            return null;
        }
    }
}