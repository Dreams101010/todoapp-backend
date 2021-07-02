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
    public class GetCompleteTasksByCategoryIdQueryUnitTests : TestBase
    {
        [Fact]
        public void Test_ReturnsCorrectCollectionSize()
        {
            // Arrange
            int categoryId = 1;
            using var connection = new NpgsqlConnection(ConnectionString);
            GetCompleteTasksByCategoryIdQuery sut = new GetCompleteTasksByCategoryIdQuery(connection);
            // Act
            var result = sut.Execute(new GetCompleteTasksByCategoryIdQueryParameter
            {
                CategoryId = categoryId,
            });
            // Assert
            Assert.Equal(2, result.Count());
            foreach (var i in result)
            {
                Assert.True(i.IsComplete);
                Assert.Equal(categoryId, i.CategoryId);
            }
        }
    }
}
