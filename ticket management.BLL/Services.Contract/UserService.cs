using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket_Management.DAL.Model;

namespace ticket_management.BLL.Services.Contract
{

        public class UserService
        {
            private readonly ApplicationDbContext _context;

            public UserService(ApplicationDbContext context)
            {
                _context = context;
            }

            public  Customer Authenticate(string email)
            {
                var user = _context.Customers
                    .FirstOrDefault(u => u.Email == email);

                return user; 
            }
        

    }
}
