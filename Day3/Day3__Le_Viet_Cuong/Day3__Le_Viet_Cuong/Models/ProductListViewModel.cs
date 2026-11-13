namespace Day3__Le_Viet_Cuong.Models
{
    public class ProductListViewModel
    {
        public IReadOnlyList<Product> Products { get; set; } = Array.Empty<Product>();
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public string? SearchTerm { get; set; }
        public string SortOrder { get; set; } = "id_asc";
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
