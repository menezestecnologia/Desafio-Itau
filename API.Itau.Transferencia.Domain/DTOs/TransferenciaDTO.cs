namespace API.Itau.Transferencia.Domain.DTOs;

public class TransferenciaDto
{
    public string ContaOrigem { get; set; } = string.Empty;
    public string ContaDestino { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}
