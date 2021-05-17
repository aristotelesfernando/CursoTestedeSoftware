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
