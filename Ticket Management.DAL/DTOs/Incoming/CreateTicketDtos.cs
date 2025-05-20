using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket_Management.DAL.DTOs.Incoming
{
    public class CreateTicketDtos
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string? MobileNumber { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [Display(Name = "Issue Type")]
        public int IssueTypeId { get; set; }
        [Display(Name = "Priority")]

        public int PriorityId { get; set; }

        public int StatusId { get; set; }

    }
}
