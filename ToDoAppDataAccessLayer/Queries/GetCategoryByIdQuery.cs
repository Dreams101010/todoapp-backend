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
    public class GetCategoryByIdQuery 
        : IQuery<GetCategoryByIdQueryParameter, Category>
    {
        private NpgsqlConnection Connection { get; set; }

        public GetCategoryByIdQuery(NpgsqlConnection connection)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            this.Connection = connection;
        }

        public Category Execute(GetCategoryByIdQueryParameter param)
        {
            string selectQuery = "SELECT id, title, color FROM category WHERE id = @Id";
            var returnValue = Connection.Query<Category>(selectQuery, param).FirstOrDefault();
            if (returnValue == null) throw new DatabaseEntryNotFoundException("Category with given id not found");
            return returnValue;
        }
    }
}
