namespace DapperExercise2;

public interface ICategoryRepository
{
    public IEnumerable<Category> GetAllCategories();
}