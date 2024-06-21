using Test2.Data;

namespace Test2.Services;

public class DbService : IDbService
{
    private readonly ApplicationContext _context;
    public DbService(ApplicationContext context)
    {
        _context = context;
    }
}