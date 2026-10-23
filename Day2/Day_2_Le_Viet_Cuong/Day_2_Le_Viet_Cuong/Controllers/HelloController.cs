using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Day_2_Le_Viet_Cuong.Controllers
{
    public class HelloController : Controller
    {
        //public string Index()
        //{
        //    return "Hello, World!";
        //}
        public IActionResult Index()
        {
            return View();
        }
        //public string Welcome(string name, int numTime = 1)
        //{
        //    return HtmlEncoder.Default.Encode($"Hello {name}, NumTime is: {numTime}");
        //}
        public IActionResult Welcome(string name, int ID = 1)
        {
            ViewBag.message = name;
            ViewData["ID"] = ID;
            return View();
        }
    }
}
