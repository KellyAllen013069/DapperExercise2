using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Linq;
using System;

namespace DapperExercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            using (IDbConnection conn = new MySqlConnection(connString))
            {
                conn.Open();  // Ensure the connection is opened
                var catRepo = new DapperCategoryRepository(conn);
                var categories = catRepo.GetAllCategories().ToArray();
                
                Console.WriteLine("Please enter a product to create:");
                var prod = Console.ReadLine();
                
                Console.WriteLine("Please enter a price for the product:");
                if (!double.TryParse(Console.ReadLine(), out double price))
                {
                    Console.WriteLine("Invalid price entered. Terminating...");
                    return;
                }
                
                Console.WriteLine("Please enter a category for the product:");
                
                foreach (var c in categories)
                {
                    Console.WriteLine($"{c.CategoryID} for {c.Name}");
                }

                var success = int.TryParse(Console.ReadLine(), out int cat);
                if (!success || cat < 1 || cat > categories.Length)
                {
                    Console.WriteLine("Invalid category entered. Terminating...");
                    return;
                }

                var prodRepo = new DapperProductRepository(conn);
                prodRepo.CreateProduct(prod, price, cat);

                var products = prodRepo.GetAllProducts();
                foreach (var p in products)
                {
                    Console.WriteLine($"ID: {p.ProductID} Name: {p.Name} Price: {p.Price}");
                }

                Console.WriteLine($"Please enter another price for {prod}:");
                if (!double.TryParse(Console.ReadLine(), out double newPrice))
                {
                    Console.WriteLine("Invalid price entered. Terminating...");
                    return;
                }

                prodRepo.UpdateProduct(prod, newPrice);

                products = prodRepo.GetAllProducts();
                foreach (var p in products)
                {
                    Console.WriteLine($"ID: {p.ProductID} Name: {p.Name} Price: {p.Price}");
                }
                
                Console.WriteLine("Deleting your new product...");
                prodRepo.DeleteProduct(prod);

                products = prodRepo.GetAllProducts();
                foreach (var p in products)
                {
                    Console.WriteLine($"ID: {p.ProductID} Name: {p.Name} Price: {p.Price}");
                }
            }
        }
    }
}
