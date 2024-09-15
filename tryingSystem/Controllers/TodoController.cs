using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Experimental.ProjectCache;
using tryingSystem.Entities;
using tryingSystem.Models;

namespace tryingSystem.Controllers
{
    public class TodoController : Controller
    {  
        private readonly AppDbContext _context;
        public TodoController(AppDbContext context)
        {
            _context =context;
        }
        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var todoItems = _context.TodoItems.Where(t => t.UserId == userId).ToList();
            return View(todoItems);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TodoItemModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                model.UserId = userId; 

                _context.TodoItems.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }else{
                Console.WriteLine("ModelState is not valid:");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
            Console.WriteLine(error.ErrorMessage);
            }
            }
            return View(model);
            }

            public IActionResult Edit(int id)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var todoItem = _context.TodoItems.FirstOrDefault(t => t.Id == id && t.UserId == userId);
                if (todoItem == null)
                {
                return NotFound();
                }
                return View(todoItem);
            }

            [HttpPost]
        public IActionResult Edit(TodoItemModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var todoItem = _context.TodoItems.FirstOrDefault(t => t.Id == model.Id && t.UserId == userId);
                if(todoItem == null)
                {
                    return NotFound();
                }
                todoItem.Title = model.Title; 
                todoItem.Description = model.Description; 
                todoItem.IsCompleted = model.IsCompleted; 

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var todoItem = _context.TodoItems.FirstOrDefault(t => t.Id == id && t.UserId == userId);
            if( todoItem == null)
            {
                return NotFound();
            }
            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}