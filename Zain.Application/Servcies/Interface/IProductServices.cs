using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zain.Application.Features;
using Zain.Domain.Entities;
using static Zain.Application.Features.Users.ProductDto;

namespace Zain.Application.Servcies.Interface
{
    public interface IProductServices
    {

        // ➕ Add
        Task<GeneralResponse<Product>> AddProductAsync(ProductFormDto dto);

        // 🔍 Get by Id
        Task<GeneralResponse<ProductQueryDto>> GetProductByIdAsync(int id);

        // 📋 Get all
        Task<GeneralResponse<IEnumerable<ProductQueryDto>>> GetAllProductsAsync();

        // ✏️ Update
        Task<GeneralResponse<Product>> UpdateProductAsync(ProductCommendDto dto);

        // ❌ Delete
        Task<GeneralResponse<Product>> DeleteProductAsync(int id);
    }
}
