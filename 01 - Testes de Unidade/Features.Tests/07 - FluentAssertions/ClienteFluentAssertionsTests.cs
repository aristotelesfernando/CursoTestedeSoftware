using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Features.Tests
{
    // Instalar o Package 
    // install-package FluentAssertions

    // Para executar os testes via console...
    // install-package xunit.runner.console
    // dotnet vstest Features.Tests.dll

    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteFluentAssertionsTests
    {
        private readonly ClienteTestsAutoMockerFixture _clienteTestsFixture;

        // Para enviar mensagens de saída nos testes
        readonly ITestOutputHelper _outputHelper;

        public ClienteFluentAssertionsTests(ClienteTestsAutoMockerFixture clienteTestsFixture, ITestOutputHelper outputHelper)
        {
            _clienteTestsFixture = clienteTestsFixture;
            _outputHelper = outputHelper;
        }

        [Fact(DisplayName = "Novo Cliente Valido")]
        [Trait("Categoria","Cliente Fluent Assertion Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {

            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert
            //Assert.True(result);
            //Assert.Equal(0, cliente.ValidationResult.Errors.Count);

            // Assert com FluentAssertion
            result.Should().BeTrue();
            cliente.ValidationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Fluent Assertion Testes")]
        public void Cliente_NovoCliente_DeveEstarInValido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            //Assert.False(result);
            //Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);

            // Assert com FluentAssertion
            result.Should().BeFalse();
            cliente.ValidationResult.Errors.Should().HaveCountGreaterOrEqualTo(1,"Explicação para a falha");

            _outputHelper.WriteLine($"Foram encontrados {cliente.ValidationResult.Errors.Count} erros nesta validação");

        }
    }
}
