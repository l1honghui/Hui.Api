
using Hui.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hui.Api.Dal
{
    public interface IDbSchemasDal : IDependency
    {
        Task<List<dynamic>> GetSchemas();

        Task<List<dynamic>> GetAllTables();

        Task<List<dynamic>> GetTable(string schemas, string tablename);

        Task<List<dynamic>> GetSchemaTables(int schemaId);

        Task<List<dynamic>> GetTableInfos(string tableName);
    }
}
