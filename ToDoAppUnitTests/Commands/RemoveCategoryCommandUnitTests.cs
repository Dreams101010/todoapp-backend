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
using ToDoAppDataAccessLayer.Commands;
using ToDoAppDomainLayer.Parameters.Commands;

namespace ToDoAppUnitTests.Commands
{
    [Collection("Sequential")]
    public class RemoveCategoryCommandUnitTests : TestBase
    {
        [Fact]
        public void Test_RemovesOnValidInput()
        {
            // Arrange
            int removedId = 3;
            using var connection = new NpgsqlConnection(ConnectionString);
            RemoveCategoryCommand sut = new RemoveCategoryCommand(connection);
            // Act
            sut.Execute(new RemoveCategoryCommandParameter
            {
               CategoryId = removedId,
            });
            // Assert
            var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM category");
            Assert.Equal(3, count);
            var removed = connection.Query<Category>("SELECT (id, title, color) FROM category " +
                "WHERE id = @Id",
                new
                {
                    Id = removedId,
                })
                .FirstOrDefault();
            Assert.Null(removed);
        }

        [Fact]
        public void Test_ThrowsOnNonExistingId()
        {
            int removedId = 6;
            using var connection = new NpgsqlConnection(ConnectionString);
            RemoveCategoryCommand sut = new RemoveCategoryCommand(connection);
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(() =>
            {
                sut.Execute(new RemoveCategoryCommandParameter
                {
                    CategoryId = removedId,
                });
            });
        }

        [Fact]
        public void Test_ThrowsOnForeignKeyConstraintViolation()
        {
            int removedId = 1;
            using var connection = new NpgsqlConnection(ConnectionString);
            RemoveCategoryCommand sut = new RemoveCategoryCommand(connection);
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(() =>
            {
                sut.Execute(new RemoveCategoryCommandParameter
                {
                    CategoryId = removedId,
                });
            });
        }
    }
}
