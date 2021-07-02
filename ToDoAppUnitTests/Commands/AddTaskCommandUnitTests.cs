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
    public class AddTaskCommandUnitTests : TestBase
    {
        [Fact]
        public void Test_InsertsOnValidInput()
        {
            // Arrange
            var title = "New task";
            var description = "New description";
            var createdAt = new DateTime(2021, 7, 2, 5, 0, 0);
            var isActive = false;
            var isComplete = false;
            var categoryId = 3;
            using var connection = new NpgsqlConnection(ConnectionString);
            AddTaskCommand sut = new AddTaskCommand(connection);
            // Act
            sut.Execute(new AddTaskCommandParameter
            {
                TaskToAdd = new ToDoTask
                {
                    Title = title,
                    Description = description,
                    CreatedAt = createdAt,
                    IsActive = isActive,
                    IsComplete = isComplete,
                    CategoryId = categoryId,
                }
            });
            // Assert
            var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM todotask");
            Assert.Equal(7, count);
            string selectQuery = "SELECT (id, title, description, createdat, " +
                "isactive, iscomplete, categoryId) FROM todotask " +
                 "WHERE " +
                 "title = @Title AND " +
                 "description = @Description AND " +
                 "createdat = @CreatedAt AND " +
                 "isactive = @IsActive AND " +
                 "iscomplete = @IsComplete AND " +
                 "categoryid = @CategoryId";
            var inserted = connection.Query<ToDoTask>(selectQuery,
                new
                {
                    Title = title,
                    Description = description,
                    CreatedAt = createdAt,
                    IsActive = isActive,
                    IsComplete = isComplete,
                    CategoryId = categoryId,
                })
                .FirstOrDefault();
            Assert.NotNull(inserted);
        }

        [Fact]
        public void Test_ThrowsOnForeignKeyConstraintViolation()
        {
            // Arrange
            var title = "New task";
            var description = "New description";
            var createdAt = new DateTime(2021, 7, 2, 5, 0, 0);
            var isActive = false;
            var isComplete = false;
            var categoryId = 5; // invalid category id
            using var connection = new NpgsqlConnection(ConnectionString);
            AddTaskCommand sut = new AddTaskCommand(connection);
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(() =>
            {
                sut.Execute(new AddTaskCommandParameter
                {
                    TaskToAdd = new ToDoTask
                    {
                        Title = title,
                        Description = description,
                        CreatedAt = createdAt,
                        IsActive = isActive,
                        IsComplete = isComplete,
                        CategoryId = categoryId,
                    }
                });
            });
        }
    }
}
