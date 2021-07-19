using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using ToDoAppDomainLayer.Interfaces;
using ToDoAppDomainLayer.Parameters.Queries;
using ToDoAppDomainLayer.DataObjects;
using ToDoAppDomainLayer.Exceptions;

namespace ToDoAppDataAccessLayer.Queries
{
    public class GetTaskByIdQuery : IQuery<GetTaskByIdQueryParameter, ToDoTaskOutputModel>
    {
        private NpgsqlConnection Connection { get; set; }

        public GetTaskByIdQuery(NpgsqlConnection connection)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            this.Connection = connection;
        }

        public ToDoTaskOutputModel Execute(GetTaskByIdQueryParameter param)
        {
            string selectQuery = "SELECT todotask.id, todotask.title, todotask.description, todotask.createdat, " +
                "todotask.isactive, todotask.iscomplete, todotask.categoryId, category.title AS categoryTitle, " +
                "category.color AS categoryColor FROM todotask JOIN category ON todotask.categoryId = category.id " +
                "WHERE todotask.id = @Id";
            var returnValue = Connection.Query<ToDoTaskOutputModel>(selectQuery, param).FirstOrDefault();
            if (returnValue == null) throw new DatabaseEntryNotFoundException("Task with given id not found");
            return returnValue;
        }
    }
}
