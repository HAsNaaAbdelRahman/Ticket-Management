using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ticket_Management.DAL.DTOs.Incoming;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;
namespace Ticket_Management.DAL.DTOs.Outgoing
{
    public class TicketForm
    {
        public CreateTicketDtos Ticket { get; set; } = new();
        public IEnumerable<SelectListItem> Priorities { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> IssueTypes { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Status { get; set; } = new List<SelectListItem>();
        public bool IsAdmin { get; set; }

    }
}
