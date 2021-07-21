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

namespace ToDoAppDataAccessLayer.Queries
{
    public class GetCompleteTasksQuery
        : IQuery<GetCompleteTasksQueryParameter, IEnumerable<ToDoTaskOutputModel>>
    {
        private NpgsqlConnection Connection { get; set; }

        public GetCompleteTasksQuery(NpgsqlConnection connection)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            this.Connection = connection;
        }
        public IEnumerable<ToDoTaskOutputModel> Execute(GetCompleteTasksQueryParameter param)
        {
            string selectQuery = "SELECT todotask.id, todotask.title, todotask.description, todotask.createdat, " +
                "todotask.isactive, todotask.iscomplete, todotask.categoryId, category.title AS categoryTitle, " +
                "category.color AS categoryColor FROM todotask JOIN category ON todotask.categoryId = category.id " +
                "WHERE iscomplete = true " +
                "ORDER BY todotask.isactive DESC, todotask.createdat DESC";
            return Connection.Query<ToDoTaskOutputModel>(selectQuery).ToList();
        }
    }
}
