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
    public class GetCategoriesQuery : IQuery<GetCategoriesQueryParameter, IEnumerable<Category>>
    {
        private NpgsqlConnection Connection { get; set; }

        public GetCategoriesQuery(NpgsqlConnection connection)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            this.Connection = connection;
        }

        public IEnumerable<Category> Execute(GetCategoriesQueryParameter param)
        {
            string selectQuery = "SELECT (id, title, color) FROM category";
            return Connection.Query<Category>(selectQuery).ToList();
        }
    }
}
