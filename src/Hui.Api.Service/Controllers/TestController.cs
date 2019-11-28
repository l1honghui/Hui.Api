using Hui.Api.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hui.Api.Bll.Service;

namespace Hui.Api.Service.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testService"></param>
        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<dynamic>>> GetAll()
        {
            var res = await _testService.QueryAsync(all => all.ToList<dynamic>());
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
            return Ok(await _testService.GetAsync(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> TestMessage()
        {
            _testService.TestMessage();
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
            return await _testService.AddAsync(testEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<int> Delete(int id)
        {
            return await _testService.RemoveAsync(id);
        }
    }
}
