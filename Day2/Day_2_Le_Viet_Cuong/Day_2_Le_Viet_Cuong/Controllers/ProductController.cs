using Microsoft.AspNetCore.Mvc;
using Day_2_Le_Viet_Cuong.Models;

namespace Day_2_Le_Viet_Cuong.Controllers
{
    // [ATTRIBUTE ROUTING] Cấu hình định tuyến bằng Attribute trên cấp Controller
    [Route("san-pham")]
    public class ProductController : Controller
    {
        // [ATTRIBUTE ROUTING] Khớp URL: /san-pham hoặc /san-pham/danh-sach
        [HttpGet("")]
        [HttpGet("danh-sach")]
        public IActionResult Index()
        {
            var products = new List<CourseItem>
            {
                new CourseItem { Id = 101, Title = "Khóa học ASP.NET Core MVC Toàn Diện", Instructor = "Bộ môn Công nghệ phần mềm", Status = "Còn chỗ" },
                new CourseItem { Id = 102, Title = "Khóa học Entity Framework Core Nâng Cao", Instructor = "Bộ môn Công nghệ phần mềm", Status = "Mới mở" },
                new CourseItem { Id = 103, Title = "Khóa học Thiết kế Web API & Microservices", Instructor = "Bộ môn Công nghệ phần mềm", Status = "Hot" }
            };

            return View(products);
        }

        // [ATTRIBUTE ROUTING] Khớp URL có ràng buộc kiểu int: /san-pham/chi-tiet/101
        [HttpGet("chi-tiet/{id:int}")]
        public IActionResult Details(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }
    }
}
