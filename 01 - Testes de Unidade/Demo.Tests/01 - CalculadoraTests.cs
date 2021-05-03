using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests
{
    public class CalculadoraTests
    {
        [Fact]
        //Observar a nomeclatura!
        public void Calculadora_Somar_RetornarValorSoma()
        {

            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(2, 2);

            // Assert
            Assert.Equal(4, resultado);
        }

        [Fact]
        public void Calculadora_Somar_RetornaDouble()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(2, 2);

            // Assert
            Assert.Equal(typeof(double), resultado.GetType());
        }

        [Theory]
        [InlineData(1,5,6)]
        [InlineData(1, 4, 5)]
        [InlineData(2, 5, 7)]
        [InlineData(1, 3, 4)]
        [InlineData(1, 1, 2)]
        public void Calculadora_Somar_RetornarValoresSomaCorreto(double v1, double v2, double total)
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(v1, v2);

            // Assert
            Assert.Equal(total, resultado);
        }

    }
}
