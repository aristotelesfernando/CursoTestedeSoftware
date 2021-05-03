using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests
{
    public class AssertingRangesTests
    {
        [Theory]
        [InlineData(500)]
        [InlineData(700)]
        [InlineData(1500)]
        [InlineData(2000)]
        [InlineData(7500)]
        [InlineData(8000)]
        [InlineData(15000)]
        public void Funcionario_Salario_FaixasSalariaisDevemRespeitarNiveisProfissionais(double salario)
        {

            // Arrange & Act
            var func = new Funcionario("Fernando", salario);

            // Assert
            if (func.NivelProfissional == NivelProfissional.Junior)
                Assert.InRange(func.Salario, 500, 1999);

            if (func.NivelProfissional == NivelProfissional.Pleno)
                Assert.InRange(func.Salario, 2000, 7999);

            if (func.NivelProfissional == NivelProfissional.Senior)
                Assert.InRange(func.Salario, 8000, double.MaxValue);

            Assert.NotInRange(func.Salario, 0, 499);
        }

        [Fact]
        public void Funcionario_Salario_FaixasSalarialValida()
        {

            // Arrange & Act
            var func = new Funcionario("Fernando", 500);

            Assert.NotInRange(func.Salario, 0, 499);
        }

    }
}
