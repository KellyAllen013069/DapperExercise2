namespace DapperExercise2;

public interface IProductRepository
{
    public IEnumerable<Product> GetAllProducts();

    public void CreateProduct(string name, double price, int categoryId );

    public void UpdateProduct(string name, double price);

    public void DeleteProduct(string name);
}