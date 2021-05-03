using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests
{
    public class AssertingObjectTypesTests
    {
        
        [Fact]
        public void FuncionarioFactory_Criar__DeveRetornarTipoFuncionario()
        {

            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Fernando", 1000);

            // Assert
            Assert.IsType<Funcionario>(funcionario);
        }

        [Fact]
        public void FuncionarioFactory_Criar_DeveRetornarTipoDerivadoPessoa()
        {

            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Fernando", 1000);

            // Assert
            Assert.IsAssignableFrom<Pessoa>(funcionario);
            // testando se a classe que foi usada para criar o objeto herda da classe assignada
        }

    }
}
