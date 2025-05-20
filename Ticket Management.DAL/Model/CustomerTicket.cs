using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ticket_Management.DAL.Model;

public partial class CustomerTicket
{
    public int TicketId { get; set; }

    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    public string? MobileNumber { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public int IssueTypeId { get; set; }

    public int PriorityId { get; set; }

    public int StatusId { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual IssueType IssueType { get; set; } = null!;

    public virtual PriorityType Priority { get; set; } = null!;

    public virtual StatusType Status { get; set; } = null!;
}
