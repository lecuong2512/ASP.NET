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
    }
}
