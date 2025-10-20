using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zain.Application.Features;
using Zain.Domain.Entities;

namespace Zain.Application.Servcies.Interface
{
    public interface ICustomerServices
    {
        // ➕ Add
        Task<GeneralResponse<Customer>> AddCustomerAsync(Customer customer);

        // 🔍 Get by Id
        Task<GeneralResponse<Customer>> GetCustomerByIdAsync(int id);

        // 📋 Get all
        Task<GeneralResponse<IEnumerable<Customer>>> GetAllCustomersAsync();

        // ✏️ Update
        Task<GeneralResponse<Customer>> UpdateCustomerAsync(Customer customer);

        // ❌ Delete
        Task<GeneralResponse<bool>> DeleteCustomerAsync(int id);
    }
}
