using Features.Clientes;
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
    // para o Automock instalar o seguinte package
    // install-package MOQ
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceTests
    {
        private readonly ClienteTestsBogusFixtures _clienteTestsBogus;

        public ClienteServiceTests(ClienteTestsBogusFixtures clienteTestsFixtures)
        {
            _clienteTestsBogus = clienteTestsFixtures;
        }

        [Fact(DisplayName = "Adicionar cliente com sucesso")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {

            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();
            var clienteRepo = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepo.Object, mediator.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            clienteRepo.Verify(r => r.Adicionar(cliente), Times.Once);
            mediator.Verify(m => m.Publish(It.IsAny<INotification>(),CancellationToken.None), Times.Once);

        }

        [Fact(DisplayName = "Adicionar cliente com falha")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteInvalido();
            var clienteRepo = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepo.Object, mediator.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido());
            clienteRepo.Verify(r => r.Adicionar(cliente), Times.Never);
            mediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }


        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            var clienteRepo = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();

            clienteRepo.Setup(c => c.ObterTodos()).Returns(_clienteTestsBogus.ObterClientesVariados());

            var clienteService = new ClienteService(clienteRepo.Object, mediator.Object);

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert
            clienteRepo.Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);
        }

    }
}
