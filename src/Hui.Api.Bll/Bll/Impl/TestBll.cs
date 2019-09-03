using Hui.Api.Bll.Bll.Base;
using Hui.Api.Common.Attributes;
using Hui.Api.Dal.Dal;
using Hui.Api.Models.Entity;
using System;

namespace Hui.Api.Bll.Bll.Impl
{
    public class TestBll : BaseBll<ITestDal, TestEntity>, ITestBll
    {
        private readonly ITestDal _testDal;

        public TestBll(ITestDal testDal) : base(testDal)
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
