using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using ToDoAppDomainLayer.Interfaces;
using ToDoAppDomainLayer.Parameters.Commands;

namespace ToDoAppDataAccessLayer.Commands
{
    public class EditCategoryCommand : ICommand<EditCategoryCommandParameter, int>
    {
        private NpgsqlConnection Connection { get; set; }
        public EditCategoryCommand(NpgsqlConnection connection)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            this.Connection = connection;
        }
        public int Execute(EditCategoryCommandParameter param)
        {
            string updateQuery = "UPDATE category " +
                "SET (title, color) = (@Title, @Color) " +
                "WHERE id = @Id";

            var affected = Connection.Execute(updateQuery, param.Category);
            if (affected == 0) throw new Exception("Update failed");
            int updatedId = param.Category.Id;
            return updatedId;
        }
    }
}
