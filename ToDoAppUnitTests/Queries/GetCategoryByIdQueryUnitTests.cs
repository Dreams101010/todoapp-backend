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
    public class GetCategoryByIdQueryUnitTests : TestBase
    {
        [Fact]
        public void Test_ReturnsCorrectCategory()
        {
            // Arrange
            using var connection = new NpgsqlConnection(ConnectionString);
            GetCategoryByIdQuery sut = new GetCategoryByIdQuery(connection);
            // Act
            var result = sut.Execute(new GetCategoryByIdQueryParameter()
            {
                Id = 1,
            });
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Test_ThrowsOnCategoryNotFound()
        {
            // Arrange
            using var connection = new NpgsqlConnection(ConnectionString);
            GetCategoryByIdQuery sut = new GetCategoryByIdQuery(connection);
            // Act
            // Assert
            Assert.Throws<DatabaseEntryNotFoundException>(() =>
            {
                var result = sut.Execute(new GetCategoryByIdQueryParameter()
                {
                    Id = 5,
                });
            });
        }
    }
}
