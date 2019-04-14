using Microsoft.EntityFrameworkCore;

namespace EFCoreOptimizationHints
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public DbSet<TestEntity> Tests { get; set; }
    }
}
