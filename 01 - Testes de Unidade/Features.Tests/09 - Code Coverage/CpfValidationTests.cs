using Features.Core;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Features.Tests
{
    public class CpfValidationTests
    {

        [Theory(DisplayName = "CPFs Válidos")]
        [Trait("Categoria","CPF Validation Tests")]
        [InlineData("15231766607")]
        [InlineData("78246847333")]
        [InlineData("64184957307")]
        [InlineData("21681764423")]
        [InlineData("13830803800")]
        public void Cpf_ValidarMultiplosNumeros_TodosDevemSerValidos(string cpf)
        {

            // Arrange
            var cpfValidation = new CpfValidation();

            // Act & Assert
            cpfValidation.EhValido(cpf).Should().BeTrue();

        }

        [Theory(DisplayName = "CPFs Válidos")]
        [Trait("Categoria", "CPF Validation Tests")]
        [InlineData("15231766627")]
        [InlineData("78246847313")]
        [InlineData("64184957300")]
        [InlineData("2168176442")]
        [InlineData("2168176442111")]
        public void Cpf_ValidarMultiplosNumeros_TodosDevemSerInvalidos(string cpf)
        {

            // Arrange
            var cpfValidation = new CpfValidation();

            // Act & Assert
            cpfValidation.EhValido(cpf).Should().BeFalse();

        }

    }
}
