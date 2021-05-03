using Bogus;
using Bogus.DataSets;
using Features.Clientes;
using System;
using Xunit;

namespace Features.Tests
{

    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection: ICollectionFixture<ClienteTestsFixture>
    {        
    }

    public class ClienteTestsFixture : IDisposable
    {

        public Cliente GerarClienteValido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            // Exemplos
            //var email = new Faker().Internet.Email("fernando", "junior");
            //var clienteFaker = new Faker<Cliente>();
            //clienteFaker.RuleFor(c => c.Nome, (f,c) => f.Name.FirstName());

            var cliente = new Cliente(
                Guid.NewGuid(),
                "Fernando",
                "Oliveira",
                DateTime.Now.AddYears(-50),
                "afernando@gmail.com",
                true,
                DateTime.Now);

            return cliente;
        }

        public Cliente GerarClienteInvalido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "afernando.com",
                true,
                DateTime.Now);

            return cliente;
        }

        public void Dispose()
        {
        }
    }
}
