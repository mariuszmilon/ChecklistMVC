using Checklist.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Controllers
{
    [Authorize]
    public class AssignmentController : Controller
    {
        private readonly AppDbContext _context;

        public AssignmentController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        //Displaying all active tasks
        
        public IActionResult Index()
        {
            var currentUser = HttpContext?.User;
            var currentUserName = currentUser.Identity.Name;
            var tasks = _context.Assignments.Where(a => (a.IsCompleted == false) && (a.Author == currentUserName)).ToList();
            return View(tasks);
        }

        //Creating Task to DB
        public IActionResult Create()
        {
            return View();
        }

        //Add Task to DB
        [HttpPost]
        public IActionResult Create(Assignment assignment)
        {
            if(!ModelState.IsValid)
                return View(assignment);

            Assignment assignmentModel = new Assignment();
            assignmentModel.Topic = assignment.Topic;
            assignmentModel.Description = assignment.Description;
            var currentUser = HttpContext?.User;
            var currentUserName = currentUser.Identity.Name;
            assignmentModel.Author = currentUserName;
            assignmentModel.IsCompleted = false;
            assignmentModel.Start = DateTime.Now;
            assignmentModel.End = assignment.End;
            _context.Add(assignmentModel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Add task to completed
        [HttpPost]
        public async Task<IActionResult> End(int id)
        {
            var task = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == id);
            task.IsCompleted = true;
            _context.Update(task);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Editing task
        public IActionResult Edit(int id)
        {
            var task = _context.Assignments.FirstOrDefault(a => a.Id == id);
            return View(task);
        }

        //Editing task
        [HttpPost]
        public IActionResult Edit(Assignment assignment)
        {
            if (!ModelState.IsValid)
                return View(assignment);

            Assignment assignmentModel = new Assignment();
            var oldTask = _context
                .Assignments
                .AsNoTracking()
                .FirstOrDefault(a => a.Id == assignment.Id);
            assignmentModel.Id = assignment.Id;
            assignmentModel.Topic = assignment.Topic;
            assignmentModel.Description = assignment.Description;
            assignment.Author = oldTask.Author;
            assignmentModel.IsCompleted = false;
            assignmentModel.Start = oldTask.Start;
            assignmentModel.End = assignment.End;
            _context.Update(assignmentModel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
