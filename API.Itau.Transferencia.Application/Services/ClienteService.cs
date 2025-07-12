using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Entidades;
using API.Itau.Transferencia.Domain.Interfaces;

namespace API.Itau.Transferencia.Application.Services;

public class ClienteService(IClienteRepository clienteRepo)
{
    private readonly IClienteRepository _clienteRepo = clienteRepo;

    public async Task<IEnumerable<Cliente>> ListarAsync() => await _clienteRepo.ListarAsync();

    public async Task<Cliente?> ObterPorConta(string numeroConta) => await _clienteRepo.ObterPorNumeroContaAsync(numeroConta);

    public async Task AdicionarAsync(ClienteDto dto)
    {
        var cliente = new Cliente(dto.Nome, dto.NumeroConta, dto.Saldo);
        await _clienteRepo.AdicionarAsync(cliente);
    }
}