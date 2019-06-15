using Hui.Api.Model.Entity.IEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hui.Api.Models.Entity
{
    /// <summary>
    /// 测试实体
    /// </summary>
    public class TestEntity : IEntity<int>
    {
        public int Id { get; set; }

    }
}
