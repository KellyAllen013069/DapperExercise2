using Microsoft.VisualBasic;

namespace DapperExercise2;

public class Product
{
    public int ProductID { get; set; }
    public string Name { get; set; } = "";
    public double Price { get; set; } = 0.0;
    public int CategoryID { get; set; } = 0;
    public bool OnSale { get; set; } = false;
    public string StockLevel { get; set; } = "";
}