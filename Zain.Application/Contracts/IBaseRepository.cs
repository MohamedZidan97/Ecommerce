using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zain.Application.Features;

namespace Zain.Application.Contracts
{
    public interface IBaseRepository<T>
    {

        // Add
        Task<GeneralResponse<T>> AddAsync(T entity);


        // Get all By Id
        Task<GeneralResponse<T>> GetByIdAsync(int id);

        // Get all
        Task<GeneralResponse<IEnumerable<T>>> GetAllAsync();

        // Update
        Task<GeneralResponse<T>> UpdateAsync(T entity);

        // Delete
        Task<GeneralResponse<T>> DeleteAsync(int id);
    }
}
