using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests
{
    public class AssertStringTests
    {
        [Fact]
        public void StringTools_UnirNomes_RetornarNomeCompleto()
        {

            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Aristoteles", "Fernando");

            // Assert
            Assert.Equal("Aristoteles Fernando", nomeCompleto);
        }

        [Fact]
        public void StringTools_UnirNomes_DeveIgnorarCase()
        {

            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Elaine", "Silva");

            // Assert
            Assert.Equal("ELAINE SILVA", nomeCompleto, true);
        }

        [Fact]
        public void StringTools_UnirNomes_DeveConterTrecho()
        {

            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Fernando", "Oliveira");

            // Assert
            Assert.Contains("ando", nomeCompleto);

        }

        [Fact]
        public void StringTools_UnirNomes_DeveComecarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Fernando", "Oliveira");

            // Assert
            Assert.StartsWith("Fer", nomeCompleto);
        }

        [Fact]
        public void StringTools_UnirNomes_DeveAcabarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Fernando", "Oliveira");

            // Assert
            Assert.EndsWith("eira", nomeCompleto);
        }

        [Fact]
        public void StringTools_UnirNomes_ValidarExpressaoRegular()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Fernando", "Oliveira");

            // Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", nomeCompleto);
        }
    }
}
