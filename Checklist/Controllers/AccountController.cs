using Microsoft.AspNetCore.Mvc;

namespace Checklist.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
