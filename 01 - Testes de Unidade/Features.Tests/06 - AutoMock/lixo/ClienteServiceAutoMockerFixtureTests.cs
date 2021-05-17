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
    public class ClienteServiceAutoMockerFixtureTests
    {
        readonly ClienteTestesAutoMockerFixtures _clienteTestsBogus;

        public ClienteServiceAutoMockerFixtureTests(ClienteTestesAutoMockerFixtures clienteTestsFixture)
        {
            _clienteTestsBogus = clienteTestsFixture;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service AutoMock Testes-2")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {

            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();
            var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            clienteRepo.Verify(r => r.Adicionar(cliente), Times.Once);
            mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service AutoMock Testes-2")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {

            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteInvalido();
            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();

            //var clienteRepo = new Mock<IClienteRepository>();
            //var mediatr = new Mock<IMediator>();
            //var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido());
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);

            //clienteRepo.Verify(r => r.Adicionar(cliente), Times.Never);
            //mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service AutoMock Testes-2")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {

            // Arrange
            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();

            //var clienteRepo = new Mock<IClienteRepository>();
            //var mediatr = new Mock<IMediator>();

            mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos()).Returns(_clienteTestsBogus.ObterClientesVariados());
            //clienteRepo.Setup(c => c.ObterTodos()).Returns(_clienteTestsBogus.ObterClientesVariados());

            //var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert
            //clienteRepo.Verify(r => r.ObterTodos(), Times.Once);
            mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);

        }

    }
}
