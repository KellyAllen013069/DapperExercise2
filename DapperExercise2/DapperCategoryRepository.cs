using System.Data;
using Dapper;

namespace DapperExercise2;

public class DapperCategoryRepository : ICategoryRepository
{
    private readonly IDbConnection _connection;

    public DapperCategoryRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public IEnumerable<Category> GetAllCategories()
    {
        return _connection.Query<Category>("SELECT * FROM categories;");
    }
}