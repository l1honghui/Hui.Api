using Hui.Api.Dal.Repositories;
using Hui.Api.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hui.Api.Dal.Dal
{
    public interface ITestDal : IRepository<TestEntity,int>
    {
    }
}
