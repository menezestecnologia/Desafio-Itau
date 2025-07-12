namespace API.Itau.Transferencia.Domain.DTOs;

public class ClienteDto
{
    public string Nome { get; set; } = string.Empty;
    public string NumeroConta { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
}
