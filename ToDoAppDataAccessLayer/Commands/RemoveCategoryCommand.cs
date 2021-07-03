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
    public class RemoveCategoryCommand : ICommand<RemoveCategoryCommandParameter, int>
    {
        private NpgsqlConnection Connection { get; set; }

        public RemoveCategoryCommand(NpgsqlConnection connection)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            this.Connection = connection;
        }

        public int Execute(RemoveCategoryCommandParameter param)
        {
            string removeQuery = "DELETE FROM category WHERE id = @CategoryId";

            var affected = Connection.Execute(removeQuery, param);
            if (affected == 0) throw new DatabaseEntryNotFoundException("Remove of category failed");
            int removedId = param.CategoryId;
            return removedId;
        }
    }
}
