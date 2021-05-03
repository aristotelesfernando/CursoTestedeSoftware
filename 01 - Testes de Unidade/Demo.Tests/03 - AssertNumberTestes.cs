using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests
{
    public class AssertNumberTestes
    {
        [Fact]
        public void Caculadora_Somar_DeveSerIgual()
        {

            // Arrange
            var calc = new Calculadora();

            // Act
            var res = calc.Somar(1, 2);

            // Assert
            Assert.Equal(3, res);
        }

        [Fact]
        public void Calculadora_Somar_NaoDeveSerIgual()
        {

            // Arrange
            var calc = new Calculadora();

            // Act
            var res = calc.Somar(1.18782988329, 2.28782778);

            // Assert
            Assert.NotEqual(3.3, res, 1);
        }

        [Fact]
        public void Calculadora_Somar_DeveSerMaiorQue()
        {

            // Arrange
            var calc = new Calculadora();

            // Act
            var res = calc.Somar(3, 3);

            // Assert
            Assert.True(res > 5);
        }

    }
}
