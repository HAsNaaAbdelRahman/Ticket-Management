using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ticket_Management.DAL.DTOs.Outgoing;
using Ticket_Management.DAL.Model;

namespace Ticket_Management.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;

        }
        [HttpGet("Dashboard")]

        public async Task<IActionResult> AdminDashBoard()
        {
            var adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
                return RedirectToAction("Index", "Home");



            var tickets = await _context.CustomerTickets

                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.IssueType)
                        .ToListAsync();

            return View(tickets);
        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _context.CustomerTickets

                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.IssueType)
                 .Include(t => t.Customer)

                .FirstOrDefaultAsync(t => t.TicketId == id);

            if (ticket == null)
                return NotFound();

            return View(ticket);
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(string? status, string? priority, string? issueType)
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin") == "true";
            var customerId = HttpContext.Session.GetInt32("CustomerId");

            if (!isAdmin && customerId == null)
                return RedirectToAction("Index", "Home");

            var query = _context.CustomerTickets

                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.IssueType)

                .AsQueryable();

            if (!isAdmin)
                query = query.Where(t => t.CustomerId == customerId);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.Status.StatusName == status);

            if (!string.IsNullOrEmpty(priority))
                query = query.Where(t => t.Priority.PriorityName == priority);

            if (!string.IsNullOrEmpty(issueType))
                query = query.Where(t => t.IssueType.IssueTypeName == issueType);

            var result = await query.ToListAsync();
            return View(result);
        }
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _context.CustomerTickets.FindAsync(id);
            if (ticket == null)
                return NotFound();

            var editForm = new EditForm
            {
                TicketId = ticket.TicketId,
                Description = ticket.Description,
                IssueTypeId = ticket.IssueTypeId,
                PriorityId = ticket.PriorityId,
                StatusId = ticket.StatusId,
                CreatedDate = ticket.CreatedDate
            };

            ViewBag.StatusList = new SelectList(await _context.StatusTypes.ToListAsync(), "StatusId", "StatusName", ticket.StatusId);
            ViewBag.PriorityList = new SelectList(await _context.PriorityTypes.ToListAsync(), "PriorityId", "PriorityName", ticket.PriorityId);
            ViewBag.IssueTypeList = new SelectList(await _context.IssueTypes.ToListAsync(), "IssueTypeId", "IssueTypeName", ticket.IssueTypeId);

            return View(editForm);
        }



        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(EditForm editForm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.StatusList = new SelectList(await _context.StatusTypes.ToListAsync(), "StatusId", "StatusName", editForm.StatusId);
                ViewBag.PriorityList = new SelectList(await _context.PriorityTypes.ToListAsync(), "PriorityId", "PriorityName", editForm.PriorityId);
                ViewBag.IssueTypeList = new SelectList(await _context.IssueTypes.ToListAsync(), "IssueTypeId", "IssueTypeName", editForm.IssueTypeId);
                return View(editForm);
            }

            var ticket = await _context.CustomerTickets.FindAsync(editForm.TicketId);
            if (ticket == null)
                return NotFound();

            ticket.Description = editForm.Description;
            ticket.IssueTypeId = editForm.IssueTypeId;
            ticket.PriorityId = editForm.PriorityId;
            ticket.StatusId = editForm.StatusId;

            await _context.SaveChangesAsync();

            return RedirectToAction("AdminDashBoard");
        }



    }
}
