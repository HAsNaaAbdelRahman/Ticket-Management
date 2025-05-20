using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using Ticket_Management.DAL.DTOs.Incoming;
using Ticket_Management.DAL.DTOs.Outgoing;
using Ticket_Management.DAL.Model;
using CustomerTicket = Ticket_Management.DAL.Model.CustomerTicket;

namespace Ticket_Management.Controllers
{
    [Route("Client")]
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Dashboard")]
        public async Task<IActionResult> ClientDashboard()
        {
            try
            {
                var customerId = HttpContext.Session.GetInt32("CustomerId");
                if (customerId == null) return RedirectToAction("Index", "Home");

                var tickets = await _context.CustomerTickets
                            .Where(t => t.CustomerId == customerId)
                            .Include(t => t.Status)
                            .Include(t => t.Priority)
                            .Include(t => t.IssueType)
                            .ToListAsync();

                return View(tickets);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); 
            }
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                bool isAdmin = HttpContext.Session.GetString("IsAdmin") == "true";

                var model = new TicketForm
                {
                    Priorities = await _context.PriorityTypes
                        .OrderBy(p => p.PriorityName)
                        .Select(p => new SelectListItem
                        {
                            Value = p.PriorityId.ToString(),
                            Text = p.PriorityName
                        })
                        .ToListAsync(),

                    IssueTypes = await _context.IssueTypes
                        .OrderBy(i => i.IssueTypeName)
                        .Select(i => new SelectListItem
                        {
                            Value = i.IssueTypeId.ToString(),
                            Text = i.IssueTypeName
                        })
                        .ToListAsync(),

                    IsAdmin = isAdmin
                };
                if (isAdmin)
                {
                    model.Status = await _context.StatusTypes
                        .Select(s => new SelectListItem
                        {
                            Value = s.StatusId.ToString(),
                            Text = s.StatusName
                        })
                        .ToListAsync();
                }

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(TicketForm model)
        {
            try
            {
                int? customerId = HttpContext.Session.GetInt32("CustomerId");
                bool isAdmin = HttpContext.Session.GetString("IsAdmin") == "true";

                if (!isAdmin)
                {
                    model.Ticket.StatusId = 1;
                }
                if (customerId == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (!ModelState.IsValid)
                {
                    model.Priorities = await _context.PriorityTypes
                        .Select(p => new SelectListItem { Value = p.PriorityId.ToString(), Text = p.PriorityName })
                        .ToListAsync();

                    model.IssueTypes = await _context.IssueTypes
                        .Select(i => new SelectListItem { Value = i.IssueTypeId.ToString(), Text = i.IssueTypeName })
                        .ToListAsync();

                    return View(model);
                }
                var ticket = new CustomerTicket
                {
                    FullName = model.Ticket.FullName,
                    Email = model.Ticket.Email,
                    MobileNumber = model.Ticket.MobileNumber,
                    Description = model.Ticket.Description,
                    CreatedDate = model.Ticket.CreatedDate,
                    IssueTypeId = model.Ticket.IssueTypeId,
                    PriorityId = model.Ticket.PriorityId,
                    StatusId = model.Ticket.StatusId,
                    CustomerId = customerId
                };

                var userEmail = HttpContext.Session.GetString("UserEmail");

                if (userEmail == "admin@gmail.com")
                {
                    model.Status = await _context.StatusTypes
                        .Select(s => new SelectListItem { Value = s.StatusId.ToString(), Text = s.StatusName })
                        .ToListAsync();
                }

                _context.CustomerTickets.Add(ticket);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ClientDashboard));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while loading the dashboard. Please try again later.");
                return View(model);
            }
        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> DetailsClient(int id)
        {
            try
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
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterClient(string? status, string? priority, string? issueType)
        {
            try
            {
                var customerId = HttpContext.Session.GetInt32("CustomerId");
                if (customerId == null)
                    return RedirectToAction("Index", "Home");

                var query = _context.CustomerTickets
                    .Include(t => t.Status)
                    .Include(t => t.Priority)
                    .Include(t => t.IssueType)
                    .Where(t => t.CustomerId == customerId)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(status))
                    query = query.Where(t => t.Status.StatusName == status);

                if (!string.IsNullOrEmpty(priority))
                    query = query.Where(t => t.Priority.PriorityName == priority);

                if (!string.IsNullOrEmpty(issueType))
                    query = query.Where(t => t.IssueType.IssueTypeName == issueType);

                var result = await query.ToListAsync();
                return View(result);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
