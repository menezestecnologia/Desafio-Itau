using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Interfaces.Repos;
using API.Itau.Transferencia.Domain.Interfaces.Services;
using API.Itau.Transferencia.Domain.Validadores;

namespace API.Itau.Transferencia.Application.Services;

public class TransferenciaService(
    IClienteRepository clienteRepo,
    ITransferenciaRepository transferenciaRepo,
    IEnumerable<IValidadorTransferencia> validadores) : ITransferenciaService
{
    private readonly IClienteRepository _clienteRepo = clienteRepo;
    private readonly ITransferenciaRepository _transferenciaRepo = transferenciaRepo;
    private readonly IEnumerable<IValidadorTransferencia> _validadores = validadores;
    private readonly SemaphoreSlim _semaforo = new(1, 1);

    public async Task RealizarAsync(TransferenciaDto dto)
    {
        await _semaforo.WaitAsync();

        try
        {
            var origem = await _clienteRepo.ObterPorNumeroContaAsync(dto.ContaOrigem);
            var destino = await _clienteRepo.ObterPorNumeroContaAsync(dto.ContaDestino);

            string status = "Sucesso";
            string? motivoFalha = null;

            foreach (var validador in _validadores)
            {
                motivoFalha = await validador.Validar(dto, origem, destino);
                if (motivoFalha != null)
                {
                    status = "Falha";
                    break;
                }
            }

            if (status == "Sucesso")
            {
                origem!.Debitar(dto.Valor);
                destino!.Creditar(dto.Valor);

                await _clienteRepo.AtualizarAsync(origem);
                await _clienteRepo.AtualizarAsync(destino);
            }

            var transferenciaOrigem = new Domain.Entidades.Transferencia(dto.ContaOrigem, dto.ContaDestino, dto.Valor, status, origem?.Id, "Envio", motivoFalha);
            await _transferenciaRepo.AdicionarAsync(transferenciaOrigem);

            var transferenciaDestino = new Domain.Entidades.Transferencia(dto.ContaOrigem, dto.ContaDestino, dto.Valor, status, destino?.Id, "Recebimento", motivoFalha);
            await _transferenciaRepo.AdicionarAsync(transferenciaDestino);
        }
        finally
        {
            _semaforo.Release();
        }
    }


    public async Task<IEnumerable<Domain.Entidades.Transferencia>> ListarPorClienteAsync(Guid clienteId)
    {
        var transferencias = await _transferenciaRepo.ListarPorClienteAsync(clienteId);
        return transferencias;
    }
}
