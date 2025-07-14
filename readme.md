# 💸 API de Transferências - Case Técnico Itaú

Este projeto simula uma API bancária responsável por:
- Cadastro e listagem de clientes
- Transferências entre contas
- Histórico de transferências (inclusive tentativas falhas)

Construído com **.NET 8**, seguindo os princípios de **Clean Architecture**, **SOLID** e boas práticas de engenharia.

---

## ✅ Funcionalidades

- [x] Cadastro e busca de clientes por número de conta
- [x] Transferência com controle de concorrência
- [x] Validações encadeadas (Chain of Responsibility)
- [x] Histórico de transferências com ordenação por data decrescente
- [x] Banco de dados InMemory (EF Core)
- [x] Testes unitários com xUnit + Moq
- [x] Testes de integração com WebApplicationFactory
- [x] Swagger (documentação da API)

---

## 🚀 Tecnologias e Padrões

- ASP.NET Core 8 (API tradicional com controllers)
- Entity Framework Core InMemory
- Clean Architecture (Domain, Application, Infrastructure, API)
- Chain of Responsibility para validações de regra de negócio
- SOLID e Clean Code aplicados
- Testes com xUnit, Moq e Microsoft.AspNetCore.Mvc.Testing

---

## ⚙️ Como rodar o projeto

### Pré-requisitos
- .NET 8 SDK instalado

### Passos

```bash
# Clonar o projeto
git clone https://github.com/menezestecnologia/Desafio-Itau
cd api.itau.transferencia

# Restaurar pacotes
dotnet restore

# Executar API
dotnet run --project src/API

# Acessar Swagger
http://localhost:5000/swagger/index.html
