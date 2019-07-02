using Hui.Api.Bll.Bll;
using Hui.Api.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hui.Api.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly ITestBll _testBll;

        public TestController(ITestBll testBll)
        {
            _testBll = testBll;
        }

        public async Task<ActionResult<List<dynamic>>> GetAll()
        {
            var res= await _testBll.QueryAsync(all => all.ToList<dynamic>());
            return Ok(res);
        }

        public async Task<ActionResult<List<dynamic>>> Query(int id)
        {
            return Ok(await _testBll.GetAsync(id));
        }


        public async Task<TestEntity> Add(TestEntity testEntity)
        {
           return await _testBll.AddAsync(testEntity);
        }

        public async Task<int> Delete(int id)
        {
            return await _testBll.RemoveAsync(id);
        }
    }
}