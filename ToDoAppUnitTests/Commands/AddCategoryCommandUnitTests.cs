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
    public class AddCategoryCommandUnitTests : TestBase
    {
        [Fact]
        public void Test_InsertsOnValidInput()
        {
            // Arrange
            var title = "Category 5";
            var color = "#abcdef";
            using var connection = new NpgsqlConnection(ConnectionString);
            AddCategoryCommand sut = new AddCategoryCommand(connection);
            // Act
            sut.Execute(new AddCategoryCommandParameter
            {
                CategoryToAdd = new Category
                {
                    Title = title,
                    Color = color,
                }
            });
            // Assert
            var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM category");
            Assert.Equal(5, count);
            var inserted = connection.Query<Category>("SELECT (id, title, color) FROM category " +
                "WHERE title = @Title AND color = @Color",
                new
                {
                    Title = title,
                    Color = color
                })
                .FirstOrDefault();
            Assert.NotNull(inserted);
        }

        [Fact]
        public void Test_ThrowsOnTryingToInsertAlreadyExistingValue()
        {
            // Arrange
            var title = "Category 1";
            var color = "#3f3f2f";
            using var connection = new NpgsqlConnection(ConnectionString);
            AddCategoryCommand sut = new AddCategoryCommand(connection);
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(() =>
            {
                sut.Execute(new AddCategoryCommandParameter
                {
                    CategoryToAdd = new Category
                    {
                        Title = title,
                        Color = color,
                    }
                });
            });
        }

        [Fact]
        public void Test_ThrowsOnInvalidColorString()
        {
            // Arrange
            var title = "Category 1";
            var color = "#3f3f2g";
            using var connection = new NpgsqlConnection(ConnectionString);
            AddCategoryCommand sut = new AddCategoryCommand(connection);
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(() =>
            {
                sut.Execute(new AddCategoryCommandParameter
                {
                    CategoryToAdd = new Category
                    {
                        Title = title,
                        Color = color,
                    }
                });
            });
        }
    }
}
