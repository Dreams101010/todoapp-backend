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
    public class EditCategoryCommandUnitTests : TestBase
    {
        [Fact]
        public void Test_UpdatesOnValidInput()
        {
            // Arrange
            var id = 1;
            var title = "Updated title";
            var color = "#123412";
            using var connection = new NpgsqlConnection(ConnectionString);
            EditCategoryCommand sut = new EditCategoryCommand(connection);
            // Act
            sut.Execute(new EditCategoryCommandParameter
            {
                Category = new Category
                {
                    Id = id,
                    Title = title,
                    Color = color,
                }
            });
            // Assert
            var updated = connection.Query<Category>("SELECT (id, title, color) FROM category " +
                "WHERE id = @Id AND title = @Title AND color = @Color",
                new
                {
                    Id = id,
                    Title = title,
                    Color = color
                })
                .FirstOrDefault();
            Assert.NotNull(updated);
        }

        [Fact]
        public void Test_ThrowsOnInvalidId()
        {
            // Arrange
            var id = 6;
            var title = "Updated title";
            var color = "#123412";
            using var connection = new NpgsqlConnection(ConnectionString);
            EditCategoryCommand sut = new EditCategoryCommand(connection);
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(() =>
            {
                sut.Execute(new EditCategoryCommandParameter
                {
                    Category = new Category
                    {
                        Id = id,
                        Title = title,
                        Color = color,
                    }
                });
            });
        }

        [Fact]
        public void Test_ThrowsOnDuplicateValue()
        {
            // Arrange
            var id = 1;
            var title = "Category 2"; // same as category 2
            var color = "#3e3e2e"; // same as category 2
            using var connection = new NpgsqlConnection(ConnectionString);
            EditCategoryCommand sut = new EditCategoryCommand(connection);
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(() =>
            {
                sut.Execute(new EditCategoryCommandParameter
                {
                    Category = new Category
                    {
                        Id = id,
                        Title = title,
                        Color = color,
                    }
                });
            });
        }

        [Fact]
        public void Test_ThrowsOnInvalidColorValue()
        {
            // Arrange
            var id = 1;
            var title = "Category 1";
            var color = "#3e3e2G"; // invalid color
            using var connection = new NpgsqlConnection(ConnectionString);
            EditCategoryCommand sut = new EditCategoryCommand(connection);
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(() =>
            {
                sut.Execute(new EditCategoryCommandParameter
                {
                    Category = new Category
                    {
                        Id = id,
                        Title = title,
                        Color = color,
                    }
                });
            });
        }

        [Fact]
        public void Test_DoesntThrowOnNothingChanged()
        {
            // Arrange
            var id = 1;
            var title = "Category 1"; // same as category 1
            var color = "#3f3f2f"; // same as category 1
            using var connection = new NpgsqlConnection(ConnectionString);
            EditCategoryCommand sut = new EditCategoryCommand(connection);
            // Act
            sut.Execute(new EditCategoryCommandParameter
            {
                Category = new Category
                {
                    Id = id,
                    Title = title,
                    Color = color,
                }
            });
            // Assert
        }
    }
}
