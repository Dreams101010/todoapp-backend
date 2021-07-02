using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Dapper;

namespace ToDoAppUnitTests
{
    public class TestBase
    {
        protected string ConnectionString { get; set; }
        public TestBase()
        {
            TestConnectionStringProvider provider = new();
            ConnectionString = provider.GetConnectionString();
            DropDatabase();
            SeedDatabase();
        }

        public void DropDatabase()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Execute("DELETE FROM todotask");
            connection.Execute("DELETE FROM category");
        }

        public void SeedDatabase()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            // insert new categories
            string insertCategoryQuery = "INSERT INTO category (id, title, color) VALUES (@Id, @Title, @Color)";
            connection.Execute(insertCategoryQuery,
                new { Id = 1, Title = "Category 1", Color = "#3f3f2f" });
            connection.Execute(insertCategoryQuery,
                new { Id = 2, Title = "Category 2", Color = "#3e3e2e" });
            connection.Execute(insertCategoryQuery,
                new { Id = 3, Title = "Category 3", Color = "#deadbe" });
            connection.Execute(insertCategoryQuery,
                new { Id = 4, Title = "Category 4", Color = "#efdead" });

            connection.Execute("ALTER SEQUENCE category_id_seq RESTART WITH 5;");
            connection.Execute("ALTER SEQUENCE todotask_id_seq RESTART WITH 1;");
            // insert new tasks

            string insertTodoTaskQuery = "INSERT INTO todotask (title, description, createdAt, " +
                "isActive, isComplete, categoryId) VALUES (@Title, @Description, @CreatedAt, " +
                "@IsActive, @IsComplete, @CategoryId)";
            connection.Execute(insertTodoTaskQuery,
                new
                {
                    Title = "Task 1",
                    Description = "Description 1",
                    CreatedAt = new DateTime(2021, 6, 29, 1, 0, 0),
                    IsActive = true,
                    IsComplete = false,
                    CategoryId = 1
                });
            connection.Execute(insertTodoTaskQuery,
                new
                {
                    Title = "Task 2",
                    Description = "Description 2",
                    CreatedAt = new DateTime(2021, 6, 29, 1, 1, 0),
                    IsActive = false,
                    IsComplete = true,
                    CategoryId = 1
                });
            connection.Execute(insertTodoTaskQuery,
                new
                {
                    Title = "Task 3",
                    Description = "Description 3",
                    CreatedAt = new DateTime(2021, 6, 29, 2, 0, 0),
                    IsActive = true,
                    IsComplete = true,
                    CategoryId = 2
                });
            connection.Execute(insertTodoTaskQuery,
                new
                {
                    Title = "Task 4",
                    Description = "Description 4",
                    CreatedAt = new DateTime(2021, 6, 29, 0, 30, 0),
                    IsActive = false,
                    IsComplete = false,
                    CategoryId = 4
                });
            connection.Execute(insertTodoTaskQuery,
                new
                {
                    Title = "Task 5",
                    Description = "Description 5",
                    CreatedAt = new DateTime(2021, 6, 15, 0, 0, 0),
                    IsActive = true,
                    IsComplete = true,
                    CategoryId = 1
                });
            connection.Execute(insertTodoTaskQuery,
                new
                {
                    Title = "Task 6",
                    Description = "Description 6",
                    CreatedAt = new DateTime(2021, 7, 21, 0, 0, 0),
                    IsActive = false,
                    IsComplete = false,
                    CategoryId = 2
                });
        }
    }
}
