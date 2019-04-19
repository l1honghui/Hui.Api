
using Hui.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hui.Api.Bll
{
    public interface IDbSchemasBll : IDependency
    {
        Task<List<dynamic>> GetSchemas();

        Task<List<dynamic>> GetAllTables();

        Task<List<dynamic>> GetTable(string schemas, string tablename);

        Task<List<dynamic>> GetSchemaTables(int schemaId);

        Task<List<dynamic>> GetTableInfos(string tableName);
    }
}
