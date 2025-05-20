using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket_Management.DAL.DTOs.Outgoing
{
    public class EditForm
    {
        public int TicketId { get; set; }
        public string Description { get; set; }
        [Display(Name = "Issue Type")]
        public int IssueTypeId { get; set; }
        [Display(Name = "Priority Type")]
        public int PriorityId { get; set; }
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}
