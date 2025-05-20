using Microsoft.AspNetCore.Identity;

namespace Ticket_Management.DAL.Data
{
    public class ApplicationUser :IdentityUser
    {
        public string FullName { get; set; }

    }
}