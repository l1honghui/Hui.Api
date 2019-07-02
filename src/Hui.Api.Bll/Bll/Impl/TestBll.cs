using Hui.Api.Bll.Bll;
using Hui.Api.Dal.Dal;
using Hui.Api.Models.Entity;

namespace Hui.Api.Bll
{
    public class TestBll : BaseBll<ITestDal, TestEntity, int>, ITestBll
    {
        private readonly ITestDal _testDal;

        public TestBll(ITestDal testDal) : base(testDal)
        {
            _testDal = testDal;
        }
    }
}
