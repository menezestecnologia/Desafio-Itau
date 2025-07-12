using API.Itau.Transferencia.Application.Services;
using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Entidades;
using API.Itau.Transferencia.Domain.Interfaces;
using API.Itau.Transferencia.Domain.Validadores;
using Moq;

namespace API.Itau.Transferencia.Tests
{
    public class TransferenciaServiceTests
    {
        [Fact]
        public void Deve_Falhar_Se_Conta_Nao_Existir()
        {
            var clienteRepo = new Mock<IClienteRepository>();
            var transferenciaRepo = new Mock<ITransferenciaRepository>();

            clienteRepo.Setup(r => r.ObterPorNumeroContaAsync(It.IsAny<string>())).ReturnsAsync((Cliente)null!);

            var validadores = new List<IValidadorTransferencia>
            {
                new ValidadorContaExistente(),
                new ValidadorLimiteValor(),
                new ValidadorSaldoSuficiente()
            };

            var service = new TransferenciaService(clienteRepo.Object, transferenciaRepo.Object, validadores);

            var dto = new TransferenciaDto
            {
                ContaOrigem = "0001",
                ContaDestino = "0002",
                Valor = 100
            };

            service.Realizar(dto);

            transferenciaRepo.Verify(r => r.AdicionarAsync(It.Is<Domain.Entidades.Transferencia>(t => t.Status == "Falha" && t.MotivoFalha == "Conta inexistente")), Times.Once);
        }

        [Fact]
        public void Deve_Falhar_Se_Valor_Acima_Limite()
        {
            var origem = new Cliente("Origem", "0001", 50000);
            var destino = new Cliente("Destino", "0002", 100);

            var clienteRepo = new Mock<IClienteRepository>();
            var transferenciaRepo = new Mock<ITransferenciaRepository>();

            clienteRepo.Setup(r => r.ObterPorNumeroContaAsync("0001")).ReturnsAsync(origem);
            clienteRepo.Setup(r => r.ObterPorNumeroContaAsync("0002")).ReturnsAsync(destino);

            var validadores = new List<IValidadorTransferencia>
            {
                new ValidadorContaExistente(),
                new ValidadorLimiteValor(),
                new ValidadorSaldoSuficiente()
            };

            var service = new TransferenciaService(clienteRepo.Object, transferenciaRepo.Object, validadores);

            var dto = new TransferenciaDto
            {
                ContaOrigem = "0001",
                ContaDestino = "0002",
                Valor = 20000
            };

            service.Realizar(dto);

            transferenciaRepo.Verify(r => r.AdicionarAsync(It.Is<Domain.Entidades.Transferencia>(t => t.Status == "Falha" && t.MotivoFalha == "Valor acima do limite")), Times.Once);
        }

        [Fact]
        public void Deve_Falhar_Se_Saldo_Insuficiente()
        {
            var origem = new Cliente("Origem", "0001", 50);
            var destino = new Cliente("Destino", "0002", 100);

            var clienteRepo = new Mock<IClienteRepository>();
            var transferenciaRepo = new Mock<ITransferenciaRepository>();

            clienteRepo.Setup(r => r.ObterPorNumeroContaAsync("0001")).ReturnsAsync(origem);
            clienteRepo.Setup(r => r.ObterPorNumeroContaAsync("0002")).ReturnsAsync(destino);

            var validadores = new List<IValidadorTransferencia>
            {
                new ValidadorContaExistente(),
                new ValidadorLimiteValor(),
                new ValidadorSaldoSuficiente()
            };

            var service = new TransferenciaService(clienteRepo.Object, transferenciaRepo.Object, validadores);

            var dto = new TransferenciaDto
            {
                ContaOrigem = "0001",
                ContaDestino = "0002",
                Valor = 200
            };

            service.Realizar(dto);

            transferenciaRepo.Verify(r => r.AdicionarAsync(It.Is<Domain.Entidades.Transferencia>(t => t.Status == "Falha" && t.MotivoFalha == "Saldo insuficiente")), Times.Once);
        }

        [Fact]
        public void Deve_Transferir_Quando_Tudo_Valido()
        {
            var origem = new Cliente("Origem", "0001", 1000);
            var destino = new Cliente("Destino", "0002", 500);

            var clienteRepo = new Mock<IClienteRepository>();
            var transferenciaRepo = new Mock<ITransferenciaRepository>();

            clienteRepo.Setup(r => r.ObterPorNumeroContaAsync("0001")).ReturnsAsync(origem);
            clienteRepo.Setup(r => r.ObterPorNumeroContaAsync("0002")).ReturnsAsync(destino);
            clienteRepo.Setup(r => r.AtualizarAsync(It.IsAny<Cliente>())).Returns(Task.CompletedTask);
            transferenciaRepo.Setup(r => r.AdicionarAsync(It.IsAny<Domain.Entidades.Transferencia>())).Returns(Task.CompletedTask);

            var validadores = new List<IValidadorTransferencia>
            {
                new ValidadorContaExistente(),
                new ValidadorLimiteValor(),
                new ValidadorSaldoSuficiente()
            };

            var service = new TransferenciaService(clienteRepo.Object, transferenciaRepo.Object, validadores);

            var dto = new TransferenciaDto
            {
                ContaOrigem = "0001",
                ContaDestino = "0002",
                Valor = 200
            };

            service.Realizar(dto);

            transferenciaRepo.Verify(r => r.AdicionarAsync(It.Is<Domain.Entidades.Transferencia>(t => t.Status == "Sucesso" && t.MotivoFalha == null)), Times.Once);
        }
    }
}