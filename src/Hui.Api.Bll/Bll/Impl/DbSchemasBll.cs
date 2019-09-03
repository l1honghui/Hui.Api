using System.Collections.Generic;
using System.Threading.Tasks;
using Hui.Api.Dal;
using Hui.Api.Dal.Dal;

namespace Hui.Api.Bll.Bll.Impl
{
    public class DbSchemasBll : IDbSchemasBll
    {
        private readonly IDbSchemasDal _dbSchemasDal;

        private readonly ITestDal _testDal;

        public DbSchemasBll(IDbSchemasDal dbSchemasDal, ITestDal testDal)
        {
            _dbSchemasDal = dbSchemasDal;
            _testDal = testDal;
        }

        public async Task Test(int id)
        {
            var entity = _testDal.GetAsync(id);
        }

        public async Task<List<dynamic>> GetAllTables()
        {
            return await _dbSchemasDal.GetAllTables();
        }

        public async Task<List<dynamic>> GetSchemaTables(int schemaId)
        {
            return await _dbSchemasDal.GetSchemaTables(schemaId);
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