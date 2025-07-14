using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Itau.Transferencia.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClienteController(IClienteService service) : ControllerBase
{
    private readonly IClienteService _service = service;

    [HttpGet]
    public async Task<IActionResult> Listar() => Ok(await _service.ListarAsync());

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] ClienteDto dto)
    {
        var sucesso = await _service.AdicionarAsync(dto);
        if (!sucesso)
            return Conflict($"Conta {dto.NumeroConta} j� existe.");

        return Created("", dto);
    }

    [HttpGet("{numeroConta}")]
    public async Task<IActionResult> Buscar(string numeroConta)
    {
        var cliente = await _service.ObterPorConta(numeroConta);
        return cliente is null ? NotFound() : Ok(cliente);
    }
}
