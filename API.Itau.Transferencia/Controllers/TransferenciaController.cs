using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Itau.Transferencia.Controllers;

[ApiController]
[Route("api/transferencias")]
public class TransferenciaController(ITransferenciaService transferenciaService, IClienteService clienteService) : ControllerBase
{
    private readonly ITransferenciaService _transferenciaService = transferenciaService;
    private readonly IClienteService _clienteService = clienteService;

    [HttpPost]
    public async Task<IActionResult> Transferir([FromBody] TransferenciaDto dto)
    {
        await _transferenciaService.RealizarAsync(dto);
        return Ok();
    }

    [HttpGet("{numeroConta}")]
    public async Task<IActionResult> Historico(string numeroConta)
    {
        var cliente = await _clienteService.ObterPorConta(numeroConta);
        if (cliente == null)
            return NotFound();

        var transferencias = await _transferenciaService.ListarPorClienteAsync(cliente.Id);
        return Ok(transferencias);
    }
}