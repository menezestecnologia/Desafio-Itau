namespace API.Itau.Transferencia.Domain.Entidades;

public class Cliente(string nome, string numeroConta, decimal saldo)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nome { get; private set; } = nome;
    public string NumeroConta { get; private set; } = numeroConta;
    public decimal Saldo { get; private set; } = saldo;
    public List<Transferencia> Transferencias { get; private set; } = [];
    public void Debitar(decimal valor) => Saldo -= valor;
    public void Creditar(decimal valor) => Saldo += valor;
}
