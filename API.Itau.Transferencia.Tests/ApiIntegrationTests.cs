using API.Itau.Transferencia.Domain.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;

namespace API.Itau.Transferencia.Tests
{
    public class ApiIntegrationTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task CriarCliente_DeveRetornar201()
        {
            var cliente = new ClienteDto
            {
                Nome = "João",
                NumeroConta = "123",
                Saldo = 1000
            };

            var response = await _client.PostAsJsonAsync("/api/clientes", cliente);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task BuscarCliente_DeveRetornar200_SeExistir()
        {
            var cliente = new ClienteDto
            {
                Nome = "Maria",
                NumeroConta = "456",
                Saldo = 2000
            };

            await _client.PostAsJsonAsync("/api/clientes", cliente);
            var response = await _client.GetAsync("/api/clientes/456");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Transferencia_DeveRetornar200_SeValida()
        {
            await _client.PostAsJsonAsync("/api/clientes", new ClienteDto
            {
                Nome = "Origem",
                NumeroConta = "1234",
                Saldo = 5000
            });

            await _client.PostAsJsonAsync("/api/clientes", new ClienteDto
            {
                Nome = "Destino",
                NumeroConta = "4321",
                Saldo = 100
            });

            var transferencia = new TransferenciaDto
            {
                ContaOrigem = "1234",
                ContaDestino = "4321",
                Valor = 500
            };

            var response = await _client.PostAsJsonAsync("/api/transferencias", transferencia);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var historico = await _client.GetAsync("/api/transferencias/1234");
            Assert.Equal(HttpStatusCode.OK, historico.StatusCode);
        }
    }
}