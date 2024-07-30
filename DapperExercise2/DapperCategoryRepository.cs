using System.Data;
using Dapper;

namespace DapperExercise2;

public class DapperCategoryRepository(IDbConnection connection) : ICategoryRepository
{
    public IEnumerable<Category> GetAllCategories()
    {
        return connection.Query<Category>("SELECT * FROM categories;");
    }
}