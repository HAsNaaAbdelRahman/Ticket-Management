using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket_Management.DAL.Model;

namespace ticket_management.BLL.Repositories.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
     
        Task<int> CompleteAsync();



    }
}
