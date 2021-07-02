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
    public class RemoveTaskCommandUnitTests : TestBase
    {
        [Fact]
        public void Test_RemovesOnValidInput()
        {
            // Arrange
            int removedId = 1;
            using var connection = new NpgsqlConnection(ConnectionString);
            RemoveTaskCommand sut = new RemoveTaskCommand(connection);
            // Act
            sut.Execute(new RemoveTaskCommandParameter
            {
                TaskId = removedId,
            });
            // Assert
            var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM todotask");
            Assert.Equal(5, count);
            var removed = connection.Query<ToDoTask>("SELECT (id, title, description, createdat, " +
                "isactive, iscomplete, categoryid) FROM todotask " +
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
            int removedId = 7; // invalidId
            using var connection = new NpgsqlConnection(ConnectionString);
            RemoveTaskCommand sut = new RemoveTaskCommand(connection);
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(() =>
            {
                sut.Execute(new RemoveTaskCommandParameter
                {
                    TaskId = removedId,
                });
            });
        }
    }
}
