using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ToDoAppUnitTests
{
    class TestConnectionStringProvider
    {
        static string ConnectionString { get; set; } = null;
        public string GetConnectionString()
        {
            if (ConnectionString == null)
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("TestConnectionStrings.json", optional: false);
                ConnectionString = builder.Build()["PostgresTestConnectionString"];
            }
            return ConnectionString;
        }
    }
}
