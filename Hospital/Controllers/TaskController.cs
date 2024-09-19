using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tryingSystem.Entities;
using tryingSystem.Models;

namespace tryingSystem.Controllers
{
    public class TaskController : Controller
    {
        private readonly AppDbContext _context;
        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles ="Nurse")]
        public IActionResult Index()
        {
            var nurseId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tasks = _context.Tasks.Where(t => t.AssignedToNurseId.ToString() == nurseId).ToList();
            return View(tasks);
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult ManageTasks()
        {
            var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tasks = _context.Tasks.Where(t => t.AssignedByDoctorId.ToString() == doctorId).ToList();
            return View(tasks);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public IActionResult AddTask(TaskInit task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return RedirectToAction("ManageTasks");
        }

        [Authorize(Roles = "Doctor")]

        public IActionResult EditTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task==null){
                return NotFound();
            }
            return View(task);
        }
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null){
                NotFound();
            }
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction("ManageTasks");
        }
        [Authorize(Roles = "Doctor")]
        public IActionResult AddTask()
        {
        ViewBag.Nurses = _context.UserAccounts.Where(u => u.Role == "Nurse").ToList();
        return View();
        }


    }
}