using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests
{
    public class AssertNullBoolTests
    {
        [Fact]
        public void Funcionario_Nome_NaoDeveSerNuloOuVazio()
        {

            // Arrange
            var func = new Funcionario("", 1000);

            // Act

            // Assert
            Assert.False(string.IsNullOrEmpty(func.Nome));
        }

        [Fact]
        public void Functionario_Apelido_NaoDeveTerApelido()
        {

            // Arrange & Act
            var func = new Funcionario("Fernando", 1000);

            // Assert
            Assert.Null(func.Apelido);

            // Assert bool
            Assert.True(string.IsNullOrEmpty(func.Apelido));
            Assert.False(func.Apelido?.Length > 0);
        }

    }
}
