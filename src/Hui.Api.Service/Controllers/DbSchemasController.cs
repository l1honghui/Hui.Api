using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hui.Api.Bll;
using Hui.Api.Models.DbSchemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hui.Api.Service.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class DbSchemasController : ControllerBase
    {
        private readonly ILogger<DbSchemasController> _logger;
        private readonly IDbSchemasBll _dbSchemasBll;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbSchemasBll"></param>
        public DbSchemasController(IDbSchemasBll dbSchemasBll, ILogger<DbSchemasController> logger)
        {
            _dbSchemasBll = dbSchemasBll;
            _logger = logger;
        }

        /// <summary>
        /// 获取所有schemas
        /// </summary>
        /// <returns></returns>
        [HttpGet("schemas")]
        public async Task<ActionResult<ResponseData<List<dynamic>>>> GetSchemas()
        {
            try
            {
                var result = await _dbSchemasBll.GetSchemas();
                return ResponseData<List<dynamic>>.Success(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                Console.WriteLine(e);
                return ResponseData<List<dynamic>>.Failure(500, e.Message, null);
            }
        }


        /// <summary>
        /// 获取schema所有table
        /// </summary>
        /// <returns></returns>
        [HttpGet("tables")]
        public async Task<ActionResult<ResponseData<List<dynamic>>>> GetSchemaTables(string schemaTable)
        {
            try
            {
                string schemas = null;
                string tablename = null;
                if (!string.IsNullOrEmpty(schemaTable))
                {
                    if (schemaTable.Contains("."))
                    {
                        var tableArr = schemaTable.Split('.');
                        schemas = tableArr[0];
                        tablename = tableArr[1];
                    }
                    else
                        tablename = schemaTable;
                }

                var result = await _dbSchemasBll.GetTable(schemas, tablename);
                return ResponseData<List<dynamic>>.Success(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                Console.WriteLine(e);
                return ResponseData<List<dynamic>>.Failure(500, e.Message, null);
            }
        }

        /// <summary>
        /// 获取table所有信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("table-info")]
        public async Task<ActionResult<ResponseData<List<dynamic>>>> GetTableInfos(string tableName)
        {
            try
            {
                var tablestrArr = tableName.Split('.');
                var querytable = tablestrArr.Length == 2 ? tablestrArr[1] : tableName;
                var result = await _dbSchemasBll.GetTableInfos(querytable);
                return ResponseData<List<dynamic>>.Success(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                Console.WriteLine(e);
                return ResponseData<List<dynamic>>.Failure(500, e.Message, null);
            }
        }
    }

}