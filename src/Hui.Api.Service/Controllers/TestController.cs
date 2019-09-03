using Hui.Api.Bll.Bll;
using Hui.Api.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hui.Api.Service.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly ITestBll _testBll;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testBll"></param>
        public TestController(ITestBll testBll)
        {
            _testBll = testBll;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<dynamic>>> GetAll()
        {
            var res = await _testBll.QueryAsync(all => all.ToList<dynamic>());
            return Ok(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<dynamic>>> Query(int id)
        {
            return Ok(await _testBll.GetAsync(id));
        }

        [HttpGet]
        public ActionResult<string> TestMessage()
        {
            _testBll.TestMessage();
            return Ok("true");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TestEntity> Add(TestEntity testEntity)
        {
            return await _testBll.AddAsync(testEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<int> Delete(int id)
        {
            return await _testBll.RemoveAsync(id);
        }
    }
}
