using API.Itau.Transferencia.Application.Services;
using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Entidades;
using API.Itau.Transferencia.Domain.Interfaces.Repos;
using Moq;

namespace API.Itau.Transferencia.Tests
{
    public class ClienteServiceTests
    {
        [Fact]
        public async Task Deve_Adicionar_Cliente_Com_Sucesso()
        {
            // Arrange
            var mockRepo = new Mock<IClienteRepository>();
            var service = new ClienteService(mockRepo.Object);

            var dto = new ClienteDto
            {
                Nome = "Carlos",
                NumeroConta = "789",
                Saldo = 1500
            };

            // Act
            await service.AdicionarAsync(dto);

            // Assert
            mockRepo.Verify(r => r.AdicionarAsync(It.Is<Cliente>(c =>c.Nome == dto.Nome && c.NumeroConta == dto.NumeroConta && c.Saldo == dto.Saldo)), Times.Once);
        }

        [Fact]
        public async Task Deve_Listar_Todos_Os_Clientes()
        {
            // Arrange
            var clientes = new List<Cliente>
            {
                new("A", "1", 100),
                new("B", "2", 200)
            };

            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.ListarAsync()).ReturnsAsync(clientes);
            var service = new ClienteService(mockRepo.Object);

            // Act
            var resultado = await service.ListarAsync();

            // Assert
            Assert.Equal(2, resultado.Count());
            Assert.Contains(resultado, c => c.NumeroConta == "1");
            Assert.Contains(resultado, c => c.NumeroConta == "2");
        }

        [Fact]
        public async Task Deve_Retornar_Cliente_Por_Conta()
        {
            // Arrange
            var cliente = new Cliente("Lucas", "321", 1000);
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.ObterPorNumeroContaAsync("321")).ReturnsAsync(cliente);
            var service = new ClienteService(mockRepo.Object);

            // Act
            var resultado = await service.ObterPorConta("321");

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Lucas", resultado!.Nome);
            Assert.Equal("321", resultado.NumeroConta);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Se_Conta_Nao_Existir()
        {
            // Arrange
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.ObterPorNumeroContaAsync("999")).ReturnsAsync((Cliente?)null);
            var service = new ClienteService(mockRepo.Object);

            // Act
            var resultado = await service.ObterPorConta("999");

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task Nao_Deve_Adicionar_Cliente_Se_Conta_Existir()
        {
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.ObterPorNumeroContaAsync("123")).ReturnsAsync(new Cliente("João", "123", 500));

            var service = new ClienteService(mockRepo.Object);
            var dto = new ClienteDto { Nome = "Outro João", NumeroConta = "123", Saldo = 1000 };

            var resultado = await service.AdicionarAsync(dto);

            Assert.False(resultado);
            mockRepo.Verify(r => r.AdicionarAsync(It.IsAny<Cliente>()), Times.Never);
        }
    }
}