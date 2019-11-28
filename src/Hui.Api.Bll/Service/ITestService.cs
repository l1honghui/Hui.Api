using Hui.Api.Bll.Service.Base;
using Hui.Api.Models.Entity;

namespace Hui.Api.Bll.Service
{
    public interface ITestService : IBaseService<TestEntity>
    {
        void TestMessage();
    }
}
