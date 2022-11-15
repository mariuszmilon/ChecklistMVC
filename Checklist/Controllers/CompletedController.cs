using Checklist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Controllers
{
    public class CompletedController : Controller
    {
        private readonly AppDbContext _context;

        public CompletedController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var currentUser = HttpContext?.User;
            var currentUserName = currentUser.Identity.Name;
            var tasks = _context.Assignments.Where(a => (a.IsCompleted == true) && (a.Author == currentUserName)).ToList();
            return View(tasks);
        }
    }
}
