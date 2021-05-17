using Features.Clientes;
using MediatR;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

// para o Automock instalar o seguinte package
// install-package MOQ.automock

namespace Features.Tests
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteServiceAutoMockerFixtureTestsComInjecao
    {
        readonly ClienteTestesAutoMockerFixtures _clienteTestesAutoMockerFixtures;

        public ClienteServiceAutoMockerFixtureTestsComInjecao(ClienteTestesAutoMockerFixtures clienteTestsFixture)
        {
            _clienteTestesAutoMockerFixtures = clienteTestsFixture;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Castegoria","Cliente Service AutoMock Tests(Com Injeção)")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {

            // Arrange
            var cliente = _clienteTestesAutoMockerFixtures.GerarClienteValido();
            // necessita passar a classe concreta e não uma interface
            var clienteService = _clienteTestesAutoMockerFixtures.ObterClienteService();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            _clienteTestesAutoMockerFixtures.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            _clienteTestesAutoMockerFixtures.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);

        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Castegoria", "Cliente Service AutoMock Tests(Com Injeção)")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {

            // Arrange
            var cliente = _clienteTestesAutoMockerFixtures.GerarClienteInvalido();
            var clienteService = _clienteTestesAutoMockerFixtures.ObterClienteService();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido());
            _clienteTestesAutoMockerFixtures.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            _clienteTestesAutoMockerFixtures.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Castegoria", "Cliente Service AutoMock Tests(Com Injeção)")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {

            // Arrange
            var clienteService = _clienteTestesAutoMockerFixtures.ObterClienteService();

            _clienteTestesAutoMockerFixtures.Mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos()).Returns(_clienteTestesAutoMockerFixtures.ObterClientesVariados());

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert
            _clienteTestesAutoMockerFixtures.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);

        }

    }
}
