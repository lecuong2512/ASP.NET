using Day3__Le_Viet_Cuong.Models;

namespace Day3__Le_Viet_Cuong.Data
{
    public class SeedData
    {
        //Gia lap du lieu tu database
        public static List<Category> categories = new List<Category>()
        {
            new Category() { Id = 1, Name = "Category 1", Description = "Description 1" },
            new Category() { Id = 2, Name = "Category 2", Description = "Description 2" },
            new Category() { Id = 3, Name = "Category 3", Description = "Description 3" }
        };

        public static List<Product> products = new List<Product>()
        {
            new Product() { Id = 1, Name = "Product 1", Price = 10.5m, Description = "Description 1", CategoryId = 1, Category = categories[0] },
            new Product() { Id = 2, Name = "Product 2", Price = 20m, Description = "Description 2", CategoryId = 2, Category = categories[1] },
            new Product() { Id = 3, Name = "Product 3", Price = 30m, Description = "Description 3", CategoryId = 3, Category = categories[2] }
        };

        static SeedData()
        {
            foreach (var product in products)
            {
                product.Category!.Products.Add(product);
            }
        }
    }
}
