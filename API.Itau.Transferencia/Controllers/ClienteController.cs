using API.Itau.Transferencia.Application.Services;
using API.Itau.Transferencia.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Itau.Transferencia.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClienteController(ClienteService service) : ControllerBase
{
    private readonly ClienteService _service = service;

    [HttpGet]
    public async Task<IActionResult> Listar() => Ok(await _service.ListarAsync());

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] ClienteDto dto)
    {
        await _service.AdicionarAsync(dto);
        return Created("", dto);
    }

    [HttpGet("{numeroConta}")]
    public async Task<IActionResult> Buscar(string numeroConta)
    {
        var cliente = await _service.ObterPorConta(numeroConta);
        return cliente is null ? NotFound() : Ok(cliente);
    }
}
