using System.Data;
using Dapper;

namespace DapperExercise2;

public class DapperProductRepository(IDbConnection connection) : IProductRepository
{
    // private readonly IDbConnection _connection;

    // public DapperProductRepository(IDbConnection connection)
    // {
    //     _connection = connection;
    // }
    
    public IEnumerable<Product> GetAllProducts()
    {
        return connection.Query<Product>("Select * FROM products;");
    }

    public void CreateProduct(string name, double price, int categoryId)
    {
        connection.Execute("INSERT INTO products (Name, Price, CategoryID) " +
                            "VALUES (@name, @price, @categoryId)",
            new {name = name, price = price, categoryId = categoryId});
    }

    public void UpdateProduct(string name, double price)
    {
        connection.Execute("UPDATE products SET Price = @price WHERE Name = @name",
            new { name, price });
    }

    public void DeleteProduct(string name)
    {
        var product = connection.QuerySingleOrDefault<Product>("SELECT * FROM products WHERE Name = @name LIMIT 1;", new { name });

        if (product == null)
        {
            Console.WriteLine("Product not found. Terminating...");
            return;
        }

        var id = product.ProductID;

        using (var transaction = connection.BeginTransaction())
        {
            try
            {
                connection.Execute("DELETE FROM sales WHERE ProductID = @id;", new { id }, transaction);
                connection.Execute("DELETE FROM reviews WHERE ProductID = @id;", new { id }, transaction);
                connection.Execute("DELETE FROM products WHERE ProductID = @id;", new { id }, transaction);
            
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }

}