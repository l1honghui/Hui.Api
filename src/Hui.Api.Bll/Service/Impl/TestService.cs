using System;
using Hui.Api.Bll.Service.Base;
using Hui.Api.Common.Attributes;
using Hui.Api.Dal.Data;
using Hui.Api.Models.Entity;

namespace Hui.Api.Bll.Service.Impl
{
    public class TestService : BaseService<ITestRepository, TestEntity>, ITestService
    {
        private readonly ITestRepository _testDal;

        public TestService(ITestRepository testDal) : base(testDal)
        {
            _testDal = testDal;
        }

        [AspectStopWatch]
        public void TestMessage()
        {
            Console.WriteLine("service TestBll calling...");
        }
    }
}
