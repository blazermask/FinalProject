using FinalProject.Data;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Tests.TestHelpers
{
    public class TestApplicationDbContext : ApplicationDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            }
        }
    }

    public static class TestDbContextFactory
    {
        public static TestApplicationDbContext Create()
        {
            var context = new TestApplicationDbContext();
            context.Database.EnsureCreated();
            return context;
        }
    }
}
