namespace API.Itau.Transferencia.Domain.Entidades;

public class Transferencia
{
    public Guid Id { get; private set; }
    public string? ContaOrigem { get; private set; }
    public string? ContaDestino { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime Data { get; private set; }
    public string? Status { get; private set; }
    public string? MotivoFalha { get; private set; }

    private Transferencia() { }

    public Transferencia(string origem, string destino, decimal valor, string status, string? motivoFalha = null)
    {
        Id = Guid.NewGuid();
        ContaOrigem = origem;
        ContaDestino = destino;
        Valor = valor;
        Status = status;
        MotivoFalha = motivoFalha;
        Data = DateTime.UtcNow;
    }
}
