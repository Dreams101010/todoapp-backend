using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using ToDoAppDomainLayer.Interfaces;
using ToDoAppDomainLayer.Parameters.Commands;
using ToDoAppDomainLayer.Exceptions;

namespace ToDoAppDataAccessLayer.Commands
{
    public class AddCategoryCommand : ICommand<AddCategoryCommandParameter, int>
    {
        private NpgsqlConnection Connection { get; set; }
        public AddCategoryCommand(NpgsqlConnection connection)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            this.Connection = connection;
        }

        public int Execute(AddCategoryCommandParameter param)
        {
            string insertQuery = "INSERT INTO category (title, color) VALUES (@Title, @Color)";
            string selectQuery = "SELECT (id) FROM category WHERE title = @Title AND color = @Color";

            var affected = Connection.Execute(insertQuery, param.CategoryToAdd);
            if (affected == 0) throw new DatabaseInteractionException("Insertion of category failed");
            int insertedId = (int)Connection.ExecuteScalar(selectQuery, param.CategoryToAdd);
            return insertedId;
        }
    }
}
