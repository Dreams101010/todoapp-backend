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
    public class AddTaskCommand : ICommand<AddTaskCommandParameter, int>
    {
        private NpgsqlConnection Connection { get; set; }
        public AddTaskCommand(NpgsqlConnection connection)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            this.Connection = connection;
        }
        public int Execute(AddTaskCommandParameter param)
        {
            string insertQuery = 
                "INSERT INTO todotask (title, description, createdat, isactive, iscomplete, categoryid) " +
                "VALUES " +
                "(@Title, @Description, @CreatedAt, @IsActive, @IsComplete, @CategoryId)";
            string selectQuery = "SELECT (id) FROM todotask " +
                "WHERE " +
                "title = @Title AND " +
                "description = @Description AND " +
                "createdat = @CreatedAt AND " +
                "isactive = @IsActive AND " +
                "iscomplete = @IsComplete AND " +
                "categoryid = @CategoryId";
            var affected = Connection.Execute(insertQuery, param.TaskToAdd);
            if (affected == 0) throw new DatabaseInteractionException("Insertion of task failed");
            int insertedId = (int)Connection.ExecuteScalar(selectQuery, param.TaskToAdd);
            return insertedId;
        }
    }
}
