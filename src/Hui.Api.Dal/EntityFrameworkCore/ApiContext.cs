using Microsoft.EntityFrameworkCore;

namespace Hui.Api.Dal.EntityFrameworkCore
{
    /// <summary>
    /// 检查/检验Context
    /// </summary>
    public class ApiContext : DbContext
    {
        /// <summary>
        ///
        /// </summary>
        public ApiContext()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
