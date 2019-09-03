using Hui.Api.Dal.Repositories;
using Hui.Api.Models.Entity;

namespace Hui.Api.Dal.Dal
{
    /// <summary>
    /// 仓储测试Dal
    /// </summary>
    public class TestDal : EfCoreRepositoryBase<ApiContext, TestEntity>, ITestDal
    {
        public TestDal(ApiContext dbContext)
            : base(dbContext)
        {
        }

    }
}
