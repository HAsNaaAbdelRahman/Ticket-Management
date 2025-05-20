using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ticket_Management.DAL.Model;

public partial class Customer
{
    public int Id { get; set; }
    [Required]

    public string FullName { get; set; } = null!;
    [Required]

    public string? MobileNumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string Password { get; set; } = null!;

    public virtual ICollection<CustomerTicket> CustomerTickets { get; set; } = new List<CustomerTicket>();
}
