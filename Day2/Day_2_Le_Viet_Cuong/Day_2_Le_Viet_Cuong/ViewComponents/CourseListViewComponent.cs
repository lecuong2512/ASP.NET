using Microsoft.AspNetCore.Mvc;
using Day_2_Le_Viet_Cuong.Models;

namespace Day_2_Le_Viet_Cuong.ViewComponents
{
    public class CourseListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title = "Danh Sách Học Phần")
        {
            var courses = new List<CourseItem>
            {
                new CourseItem { Id = 1, Title = "ASP.NET Core MVC Cơ Bản", Instructor = "Bộ môn Công nghệ phần mềm", Status = "Đã hoàn thành" },
                new CourseItem { Id = 2, Title = "Views, Partial Views & ViewComponents", Instructor = "Bộ môn Công nghệ phần mềm", Status = "Đang thực hành" },
                new CourseItem { Id = 3, Title = "Entity Framework Core & SQL Server", Instructor = "Bộ môn Công nghệ phần mềm", Status = "Sắp học" }
            };

            ViewBag.ComponentTitle = title;
            return View(courses);
        }
    }
}
