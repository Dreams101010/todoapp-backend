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
    public class GetCompleteTasksQueryUnitTests : TestBase
    {
        [Fact]
        public void Test_ReturnsCorrectCollectionSize()
        {
            // Arrange
            using var connection = new NpgsqlConnection(ConnectionString);
            GetCompleteTasksQuery sut = new GetCompleteTasksQuery(connection);
            // Act
            var result = sut.Execute(new GetCompleteTasksQueryParameter());
            // Assert
            Assert.Equal(3, result.Count());
            foreach (var i in result)
            {
                Assert.True(i.IsComplete);
            }
        }
    }
}
