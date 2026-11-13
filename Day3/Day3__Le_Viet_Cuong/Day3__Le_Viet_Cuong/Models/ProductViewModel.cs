using Microsoft.AspNetCore.Mvc.Rendering;

namespace Day3__Le_Viet_Cuong.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; } = new();
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
