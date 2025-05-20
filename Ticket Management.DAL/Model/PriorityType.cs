using System;
using System.Collections.Generic;

namespace Ticket_Management.DAL.Model;

public partial class PriorityType
{
    public int PriorityId { get; set; }

    public string PriorityName { get; set; } = null!;

    public virtual ICollection<CustomerTicket> CustomerTickets { get; set; } = new List<CustomerTicket>();
}
