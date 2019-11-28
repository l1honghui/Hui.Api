using System.Collections.Generic;
using System.Threading.Tasks;
using Hui.Api.Models;

namespace Hui.Api.Bll.Service
{
    public interface IDbSchemasService : IDependency
    {
        Task<List<dynamic>> GetSchemas();

        Task<List<dynamic>> GetAllTables();

        Task<List<dynamic>> GetTable(string schemas, string tableName);

        Task<List<dynamic>> GetSchemaTables(int schemaId);

        Task<List<dynamic>> GetTableInfos(string tableName);
    }
}
