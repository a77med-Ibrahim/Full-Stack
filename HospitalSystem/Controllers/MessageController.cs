using System.Security.Claims;
using HospitalSystem.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HospitalSystem.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MessageController> _logger;

        public MessageController(AppDbContext context, ILogger<MessageController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInformation("GET Create - Rendering the message creation view.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string text)
        {
            _logger.LogInformation("POST Create - Attempting to create a new message.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("POST Create - ModelState is invalid.");
                TempData["Message"] = "There was a problem with the input data.";
                return View();
            }

            try
            {
                var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                _logger.LogInformation("POST Create - Doctor ID: {DoctorId}", doctorId);

                var message = new Message
                {
                    Text = text,
                    DoctorId = doctorId,
                    CreatedAt = DateTime.Now
                };

                _context.Messages.Add(message);
                _context.SaveChanges();

                _logger.LogInformation("POST Create - Message created successfully and saved to the database.");
                TempData["Message"] = "Message created successfully.";
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST Create - Error occurred while creating the message.");
                TempData["Message"] = "There was an error saving the message.";
                return View();
            }
        }

        public IActionResult ViewMessages()
        {
            _logger.LogInformation("ViewMessages - Retrieving messages from the database.");
            var messages = _context.Messages.Include(m => m.Doctor).ToList();
            return View(messages);
        }
    }
}
