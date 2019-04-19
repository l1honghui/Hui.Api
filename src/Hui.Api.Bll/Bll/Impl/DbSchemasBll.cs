using Hui.Api.Dal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hui.Api.Bll.Impl
{
    public class DbSchemasBll: IDbSchemasBll
    { 
        private IDbSchemasDal _dbSchemasDal;

        public DbSchemasBll(IDbSchemasDal dbSchemasDal)
        {
            _dbSchemasDal = dbSchemasDal;
        }

        public async Task<List<dynamic>> GetAllTables()
        {
            return await _dbSchemasDal.GetAllTables();
        }

        public async Task<List<dynamic>> GetSchemaTables(int schemaId)
        {
            return  await _dbSchemasDal.GetSchemaTables(schemaId);
        }

        public async Task<List<dynamic>> GetSchemas()
        {
            return await _dbSchemasDal.GetSchemas();
        }

        public async Task<List<dynamic>> GetTableInfos(string tableName)
        {
            return await _dbSchemasDal.GetTableInfos(tableName);
        }

        public async Task<List<dynamic>> GetTable(string schemas, string tablename)
        {
            return await _dbSchemasDal.GetTable(schemas, tablename);
        }
    }
}