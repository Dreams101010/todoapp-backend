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
using ToDoAppDomainLayer.Exceptions;

namespace ToDoAppUnitTests.Queries
{
    [Collection("Sequential")]
    public class GetTaskByIdQueryUnitTests : TestBase
    {
        [Fact]
        public void Test_ReturnsCorrectTask()
        {
            // Arrange
            using var connection = new NpgsqlConnection(ConnectionString);
            GetTaskByIdQuery sut = new GetTaskByIdQuery(connection);
            // Act
            var result = sut.Execute(new GetTaskByIdQueryParameter()
            {
                Id = 1,
            });
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Test_ThrowsOnTaskNotFound()
        {
            // Arrange
            using var connection = new NpgsqlConnection(ConnectionString);
            GetTaskByIdQuery sut = new GetTaskByIdQuery(connection);
            // Act
            // Assert
            Assert.Throws<DatabaseEntryNotFoundException>(() =>
            {
                var result = sut.Execute(new GetTaskByIdQueryParameter()
                {
                    Id = 7,
                });
            });
        }
    }
}
