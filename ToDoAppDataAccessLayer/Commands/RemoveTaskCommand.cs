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
    public class RemoveTaskCommand : ICommand<RemoveTaskCommandParameter, int>
    {
        private NpgsqlConnection Connection { get; set; }

        public RemoveTaskCommand(NpgsqlConnection connection)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            this.Connection = connection;
        }

        public int Execute(RemoveTaskCommandParameter param)
        {
            string removeQuery = "DELETE FROM todotask WHERE id = @TaskId";

            var affected = Connection.Execute(removeQuery, param);
            if (affected == 0) throw new Exception("Remove failed");
            int removedId = param.TaskId;
            return removedId;
        }
    }
}
