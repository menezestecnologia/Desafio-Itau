using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Itau.Transferencia.Controllers;

[ApiController]
[Route("api/transferencias")]
public class TransferenciaController(ITransferenciaService service) : ControllerBase
{
    private readonly ITransferenciaService _service = service;

    [HttpPost]
    public async Task<IActionResult> Transferir([FromBody] TransferenciaDto dto)
    {
        await _service.RealizarAsync(dto);
        return Ok();
    }

    [HttpGet("{numeroConta}")]
    public async Task<IActionResult> Historico(string numeroConta)
    {
        var historico = await _service.ObterHistorico(numeroConta);
        return Ok(historico);
    }
}