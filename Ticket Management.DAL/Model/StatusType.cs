using System;
using System.Collections.Generic;

namespace Ticket_Management.DAL.Model;

public partial class StatusType
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<CustomerTicket> CustomerTickets { get; set; } = new List<CustomerTicket>();
}
