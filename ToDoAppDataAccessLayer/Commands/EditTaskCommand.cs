using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using ToDoAppDomainLayer.Interfaces;
using ToDoAppDomainLayer.Parameters.Commands;
using ToDoAppDomainLayer.DataObjects;
using ToDoAppDomainLayer.Exceptions;

namespace ToDoAppDataAccessLayer.Commands
{
    public class EditTaskCommand : ICommand<EditTaskCommandParameter, int>
    {
        private NpgsqlConnection Connection { get; set; }
        public EditTaskCommand(NpgsqlConnection connection)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            this.Connection = connection;
        }
        public int Execute(EditTaskCommandParameter param)
        {
            string updateQuery =
                "UPDATE todotask SET (title, description, createdat, isactive, iscomplete, categoryid) = " +
                "(@Title, @Description, @CreatedAt, @IsActive, @IsComplete, @CategoryId) WHERE id = @Id";
            var affected = Connection.Execute(updateQuery, param.Task);
            if (affected == 0) throw new DatabaseEntryNotFoundException("Update of task failed");
            int updatedId = param.Task.Id;
            return updatedId;
        }
    }
}
