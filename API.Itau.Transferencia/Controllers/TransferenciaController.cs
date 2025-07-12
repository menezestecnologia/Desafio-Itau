using API.Itau.Transferencia.Application.Services;
using API.Itau.Transferencia.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Itau.Transferencia.Controllers;

[ApiController]
[Route("api/transferencias")]
public class TransferenciaController(TransferenciaService service) : ControllerBase
{
    private readonly TransferenciaService _service = service;

    [HttpPost]
    public IActionResult Transferir([FromBody] TransferenciaDto dto)
    {
        _service.Realizar(dto);
        return Ok();
    }

    [HttpGet("{numeroConta}")]
    public async Task<IActionResult> Historico(string numeroConta)
    {
        var historico = await _service.ObterHistorico(numeroConta);
        return Ok(historico);
    }
}