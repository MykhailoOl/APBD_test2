using Microsoft.EntityFrameworkCore;

namespace Test2.Data;

public class ApplicationContext : DbContext
{
    protected ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    //public DbSet<Class> name{get;set;}
}