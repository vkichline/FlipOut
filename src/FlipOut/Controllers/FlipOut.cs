using Microsoft.AspNet.Mvc;
using FlipOut.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FlipOut.Controllers
{
    public class FlipOut : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Board = new Board();
            return View();
        }
    }
}
