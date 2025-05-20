using System;
using System.Collections.Generic;

namespace Ticket_Management.DAL.Model;

public partial class IssueType
{
    public int IssueTypeId { get; set; }

    public string IssueTypeName { get; set; } = null!;

    public virtual ICollection<CustomerTicket> CustomerTickets { get; set; } = new List<CustomerTicket>();
}
