using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Entidades;
using API.Itau.Transferencia.Domain.Interfaces.Repos;
using API.Itau.Transferencia.Domain.Interfaces.Services;

namespace API.Itau.Transferencia.Application.Services;

public class ClienteService(IClienteRepository clienteRepo) : IClienteService
{
    private readonly IClienteRepository _clienteRepo = clienteRepo;

    public async Task<IEnumerable<Cliente>> ListarAsync() => await _clienteRepo.ListarAsync();

    public async Task<Cliente?> ObterPorConta(string numeroConta) =>
        await _clienteRepo.ObterPorNumeroContaAsync(numeroConta);

    public async Task<bool> AdicionarAsync(ClienteDto dto)
    {
        var existente = await _clienteRepo.ObterPorNumeroContaAsync(dto.NumeroConta);
        if (existente != null)
            return false;

        var cliente = new Cliente(dto.Nome, dto.NumeroConta, dto.Saldo);
        await _clienteRepo.AdicionarAsync(cliente);
        return true;
    }
}