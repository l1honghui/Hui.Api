using Hui.Api.Dal.Repositories;
using Hui.Api.Models.Entity;

namespace Hui.Api.Dal.Data.Impl
{
    /// <summary>
    /// 仓储测试Dal
    /// </summary>
    public class TestRepository : EfCoreRepository<ApiContext, TestEntity>, ITestRepository
    {
        public TestRepository(ApiContext dbContext)
            : base(dbContext)
        {
        }

    }
}
