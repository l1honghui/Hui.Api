using Hui.Api.Dal.Repositories;
using Hui.Api.Models;
using Hui.Api.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hui.Api.Dal.Dal
{
    /// <summary>
    /// 仓储测试Dal
    /// </summary>
    public class TestDal : EfCoreRepositoryBase<ApiContext, TestEntity, int>, ITestDal
    {
        public TestDal(ApiContext dbContext)
            : base(dbContext)
        {
        }

    }
}
