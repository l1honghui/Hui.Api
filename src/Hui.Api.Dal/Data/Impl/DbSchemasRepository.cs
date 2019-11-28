using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Hui.Api.Dal.Dapper;

namespace Hui.Api.Dal.Data.Impl
{
    public class DbSchemasRepository : IDbSchemasRepository
    {
        public async Task<List<dynamic>> GetAllDataBase()
        {
            return await DapperHelper<dynamic>.QueryAsync("SELECT * FROM PG_DATABASE");
        }

        public async Task<List<dynamic>> GetSchemas()
        {
            return await DapperHelper<dynamic>.QueryAsync("SELECT oid,nspname as schema FROM pg_namespace ORDER BY nspname;");
        }

        public async Task<List<dynamic>> GetAllTables()
        {
            //return await DapperHelper<dynamic>.QueryAsync(@"SELECT tablename FROM pg_tables ORDER BY tablename");
            return await DapperHelper<dynamic>.QueryAsync(@"SELECT concat_ws('.',n.nspname,c.relname) as tablename, d.description FROM pg_class  c
left join pg_namespace n on n.oid = c. relnamespace
left JOIN pg_description d ON C.oid = d.objoid and d.objsubid = '0'
WHERE c.relpersistence = 'p' AND c.relkind = 'r' order by tablename");
        }

        public async Task<List<dynamic>> GetSchemaTables(int schemaId)
        {
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("relnamespace", schemaId);
            return await DapperHelper<dynamic>.QueryAsync(@"SELECT c.relname AS tableName, d.description
                                                    FROM pg_class c
                                                        LEFT JOIN pg_namespace n ON n.oid = c.relnamespace
                                                        LEFT JOIN pg_description d ON c.oid = d.objoid
                                                    WHERE d.objsubid = 0
                                                        AND c.relnamespace = @relnamespace ORDER BY C.relname", dynamicParams);
        }

        public async Task<List<dynamic>> GetTable(string schemas = null, string tableName = null)
        {
            if (!string.IsNullOrEmpty(tableName))
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("tablename", tableName.ToLower());
                //return await DapperHelper<dynamic>.QueryAsync(@"SELECT tablename FROM pg_tables WHERE tablename = @tablename ORDER BY tablename", dynamicParams);
                return await DapperHelper<dynamic>.QueryAsync(@"SELECT concat_ws('.',n.nspname,c.relname) as tablename, d.description FROM pg_class  c
                    left join pg_namespace n on n.oid = c. relnamespace
                    left JOIN pg_description d ON C.oid = d.objoid and d.objsubid = 0
                    WHERE c.relpersistence = 'p' AND c.relkind = 'r' and c.relname like CONCAT('%',@tablename,'%') ORDER BY tablename", dynamicParams);
            }
            else if (!string.IsNullOrEmpty(schemas))
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("nspname", schemas.ToLower());
                //return await DapperHelper<dynamic>.QueryAsync(@"SELECT tablename FROM pg_tables WHERE tablename = @tablename ORDER BY tablename", dynamicParams);
                return await DapperHelper<dynamic>.QueryAsync(@"SELECT concat_ws('.',n.nspname,c.relname) as tablename, d.description FROM pg_class  c
                    left join pg_namespace n on n.oid = c. relnamespace
                    left JOIN pg_description d ON C.oid = d.objoid and d.objsubid = 0
                    WHERE c.relpersistence = 'p' AND c.relkind = 'r' and n.nspname like CONCAT('%',@nspname,'%') ORDER BY tablename", dynamicParams);
            }
            else
                return await GetAllTables();

        }

        public async Task<List<dynamic>> GetTableInfos(string tableName)
        {
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("relname", tableName.ToLower());
            return await DapperHelper<dynamic>.QueryAsync(@"SELECT a.attname AS columnName, format_type(a.atttypid, a.atttypmod) AS type, a.attnotnull as notnull
                                                        , col_description(a.attrelid, a.attnum) AS description
                                                    FROM pg_attribute a
                                                        LEFT JOIN pg_class c ON a.attrelid = c.oid
                                                    WHERE c.relname = @relname
                                                        AND a.attnum > 0 AND a.atttypid != 0", dynamicParams);
        }




    }
}
