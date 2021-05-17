using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteBogusTests
    {
        private readonly ClienteTestsBogusFixtures _clienteTestsBogusFixtures;

        public ClienteBogusTests(ClienteTestsBogusFixtures clienteTestsBogusFixtures)
        {
            _clienteTestsBogusFixtures = clienteTestsBogusFixtures;
        }

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria","Cliente Bogus Testes")]
        public void Cliente_NovoCliente_DeveEstarVaalido()
        {

            // Arrange
            var cliente = _clienteTestsBogusFixtures.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Bogus Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {

            // Arrange
            var cliente = _clienteTestsBogusFixtures.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.False(result);
            Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
        }
    }
}
