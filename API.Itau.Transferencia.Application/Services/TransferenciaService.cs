using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Interfaces;
using API.Itau.Transferencia.Domain.Validadores;

namespace API.Itau.Transferencia.Application.Services;

public class TransferenciaService(
    IClienteRepository clienteRepo,
    ITransferenciaRepository transferenciaRepo,
    IEnumerable<IValidadorTransferencia> validadores)
{
    private readonly IClienteRepository _clienteRepo = clienteRepo;
    private readonly ITransferenciaRepository _transferenciaRepo = transferenciaRepo;
    private readonly IEnumerable<IValidadorTransferencia> _validadores = validadores;
    private readonly object _lock = new();

    public void Realizar(TransferenciaDto dto)
    {
        lock (_lock)
        {
            var origemTask = _clienteRepo.ObterPorNumeroContaAsync(dto.ContaOrigem);
            var destinoTask = _clienteRepo.ObterPorNumeroContaAsync(dto.ContaDestino);
            Task.WaitAll(origemTask, destinoTask);
            var origem = origemTask.Result;
            var destino = destinoTask.Result;

            string status = "Sucesso";
            string? motivoFalha = null;

            foreach (var validador in _validadores)
            {
                motivoFalha = validador.Validar(dto, origem, destino).Result;
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
                _clienteRepo.AtualizarAsync(origem).Wait();
                _clienteRepo.AtualizarAsync(destino).Wait();
            }

            var transferencia = new Domain.Entidades.Transferencia(dto.ContaOrigem, dto.ContaDestino, dto.Valor, status, motivoFalha);
            _transferenciaRepo.AdicionarAsync(transferencia).Wait();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Transferencia>> ObterHistorico(string numeroConta)
    {
        return await _transferenciaRepo.ObterPorContaAsync(numeroConta);
    }
}
