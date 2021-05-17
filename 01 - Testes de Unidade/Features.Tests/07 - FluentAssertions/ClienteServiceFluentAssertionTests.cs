using Features.Clientes;
using FluentAssertions;
using FluentAssertions.Extensions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteServiceFluentAssertionTests
    {

        readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockerFixture;
        private readonly ClienteService _clienteService;

        public ClienteServiceFluentAssertionTests(ClienteTestsAutoMockerFixture clienteTestsAutoMockerFixture)
        {
            _clienteTestsAutoMockerFixture = clienteTestsAutoMockerFixture;
            _clienteService = _clienteTestsAutoMockerFixture.ObterClienteService();
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria","Cliente Service Fluent Assertions Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {

            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert com FluentAssertion
            cliente.EhValido().Should().BeTrue();

            // Assert
            Assert.True(cliente.EhValido());
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service Fluent Assertions Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {

            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteInvalido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            cliente.EhValido().Should().BeFalse("Possui inconsistências");
            cliente.ValidationResult.Errors.Should().HaveCountGreaterOrEqualTo(1);


            // Assert
            Assert.False(cliente.EhValido());
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service Fluent Assertions Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {

            // Arrange
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos()).Returns(_clienteTestsAutoMockerFixture.ObterClientesVariados());


            // Act
            var clientes = _clienteService.ObterTodosAtivos();

            // Assert
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);

            // Assert
            clientes.Should().HaveCountGreaterOrEqualTo(1).And.OnlyHaveUniqueItems();
            clientes.Should().NotContain(c => !c.Ativo);

            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);

            // Verificando se a execução do retorno da execução do método é inferior a 50 minilissegundos
            _clienteService.ExecutionTimeOf(c => c.ObterTodosAtivos())
                .Should()
                .BeLessOrEqualTo(50.Milliseconds(),"porque é executado milhares de vezes por segundo");

        }
    }
}
