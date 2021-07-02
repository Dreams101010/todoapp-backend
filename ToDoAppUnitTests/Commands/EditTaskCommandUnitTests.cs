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
    public class EditTaskCommandUnitTests : TestBase
    {
        [Fact]
        public void Test_UpdatesOnValidInput()
        {
            // Arrange
            var id = 1;
            var title = "Updated task";
            var description = "Updated description";
            var createdAt = new DateTime(2000, 7, 2, 5, 0, 0);
            var isActive = true;
            var isComplete = true;
            var categoryId = 1;
            using var connection = new NpgsqlConnection(ConnectionString);
            EditTaskCommand sut = new EditTaskCommand(connection);
            // Act
            sut.Execute(new EditTaskCommandParameter
            {
                Task = new ToDoTask
                {
                    Id = id,
                    Title = title,
                    Description = description,
                    CreatedAt = createdAt,
                    IsActive = isActive,
                    IsComplete = isComplete,
                    CategoryId = categoryId
                }
            });
            // Assert
            string selectQuery = 
                "SELECT (id, title, description, createdat, " +
                "isactive, iscomplete, categoryid) FROM todotask " +
                "WHERE " +
                "title = @Title AND " +
                "description = @Description AND " +
                "createdat = @CreatedAt AND " +
                "isactive = @IsActive AND " +
                "iscomplete = @IsComplete AND " +
                "categoryid = @CategoryId"; ;
            var updated = connection.Query<Category>(selectQuery,
                new
                {
                    Id = id,
                    Title = title,
                    Description = description,
                    CreatedAt = createdAt,
                    IsActive = isActive,
                    IsComplete = isComplete,
                    CategoryId = categoryId,
                })
                .FirstOrDefault();
            Assert.NotNull(updated);
        }

        [Fact]
        public void Test_ThrowsOnInvalidId()
        {
            // Arrange
            var id = 7; // invalid id
            var title = "Updated task";
            var description = "Updated description";
            var createdAt = new DateTime(2000, 7, 2, 5, 0, 0);
            var isActive = true;
            var isComplete = true;
            var categoryId = 1;
            using var connection = new NpgsqlConnection(ConnectionString);
            EditTaskCommand sut = new EditTaskCommand(connection);
            // Act
            Assert.ThrowsAny<Exception>(() =>
            {
                sut.Execute(new EditTaskCommandParameter
                {
                    Task = new ToDoTask
                    {
                        Id = id,
                        Title = title,
                        Description = description,
                        CreatedAt = createdAt,
                        IsActive = isActive,
                        IsComplete = isComplete,
                        CategoryId = categoryId
                    }
                });
            });
        }

        [Fact]
        public void Test_ThrowsOnForeignKeyConstraintViolation()
        {
            // Arrange
            var id = 1; 
            var title = "Updated task";
            var description = "Updated description";
            var createdAt = new DateTime(2000, 7, 2, 5, 0, 0);
            var isActive = true;
            var isComplete = true;
            var categoryId = 5; // invalid id
            using var connection = new NpgsqlConnection(ConnectionString);
            EditTaskCommand sut = new EditTaskCommand(connection);
            // Act
            Assert.ThrowsAny<Exception>(() =>
            {
                sut.Execute(new EditTaskCommandParameter
                {
                    Task = new ToDoTask
                    {
                        Id = id,
                        Title = title,
                        Description = description,
                        CreatedAt = createdAt,
                        IsActive = isActive,
                        IsComplete = isComplete,
                        CategoryId = categoryId
                    }
                });
            });
        }
    }
}
