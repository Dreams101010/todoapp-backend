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
    public class GetCategoriesQueryUnitTests : TestBase
    {
        [Fact]
        public void Test_ReturnsCorrectCollectionSize()
        {
            // Arrange
            using var connection = new NpgsqlConnection(ConnectionString);
            GetCategoriesQuery sut = new GetCategoriesQuery(connection);
            // Act
            var result = sut.Execute(new GetCategoriesQueryParameter());
            // Assert
            Assert.Equal(4, result.Count());
        }
    }
}
