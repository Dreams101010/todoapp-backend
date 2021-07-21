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
    public class GetActiveTasksQuery
        : IQuery<GetActiveTasksQueryParameter, IEnumerable<ToDoTaskOutputModel>>
    {
        private NpgsqlConnection Connection { get; set; }

        public GetActiveTasksQuery(NpgsqlConnection connection)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            this.Connection = connection;
        }
        public IEnumerable<ToDoTaskOutputModel> Execute(GetActiveTasksQueryParameter param)
        {
            string selectQuery = "SELECT todotask.id, todotask.title, todotask.description, todotask.createdat, " +
                "todotask.isactive, todotask.iscomplete, todotask.categoryId, category.title AS categoryTitle, " +
                "category.color AS categoryColor FROM todotask JOIN category ON todotask.categoryId = category.id " +
                "WHERE isactive = true " +
                "ORDER BY todotask.createdat DESC";
            return Connection.Query<ToDoTaskOutputModel>(selectQuery, param).ToList();
        }
    }
}
