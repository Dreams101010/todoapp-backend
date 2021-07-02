using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Npgsql;
using Dapper;
using ToDoAppDomainLayer.DataObjects;
using ToDoAppDataAccessLayer.Queries;
using ToDoAppDomainLayer.Parameters.Queries;

namespace ToDoAppUnitTests.Queries
{
    [Collection("Sequential")]
    public class GetTasksByCategoryIdQueryUnitTests : TestBase
    {
        [Fact]
        public void Test_ReturnsCorrectCollectionSize()
        {
            // Arrange
            using var connection = new NpgsqlConnection(ConnectionString);
            GetTasksByCategoryIdQuery sut = new GetTasksByCategoryIdQuery(connection);
            // Act
            var result = sut.Execute(new GetTasksByCategoryIdQueryParameter
            {
                CategoryId = 1,
            });
            // Assert
            Assert.Equal(3, result.Count());
        }
    }
}
