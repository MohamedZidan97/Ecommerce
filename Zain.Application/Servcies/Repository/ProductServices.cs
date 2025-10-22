using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zain.Application.Contracts;
using Zain.Application.Features;
using Zain.Application.Features.Users;
using Zain.Application.IUnit;
using Zain.Application.Servcies.Interface;
using Zain.Domain.Entities;
using static Zain.Application.Features.Users.ProductDto;

namespace Zain.Application.Servcies.Repository
{
    public class ProductServices : IProductServices
    {
        private readonly IBaseRepository<Product> _baseRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductServices(IBaseRepository<Product> baseRepo,IUnitOfWork unitOfWork, IMapper mapper)
        {
            _baseRepo = baseRepo;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // ➕ Add
        public async Task<GeneralResponse<Product>> AddProductAsync(ProductFormDto dto)
        {
            var Product = mapper.Map<Product>(dto);

            var response = await _baseRepo.AddAsync(Product);

            if (!response.Success)
                return GeneralResponse<Product>.FailResponse("Adding the Product is fail");

           await unitOfWork.SaveChangesAsync();

            return response;
        }

        // 🔍 GetById
        public async Task<GeneralResponse<ProductQueryDto>> GetProductByIdAsync(int id)
        {
            var response = await _baseRepo.GetByIdAsync(id);
            if (!response.Success)
                return GeneralResponse<ProductQueryDto>.FailResponse("Product not found");

            var dto = mapper.Map<ProductQueryDto>(response.Data);
            var result = GeneralResponse<ProductQueryDto>.SuccessResponse(dto,response.Message);

            return result;
        }


        // 📋 GetAll
        public async Task<GeneralResponse<IEnumerable<ProductQueryDto>>> GetAllProductsAsync()
        {
            var response = await _baseRepo.GetAllAsync();

            var dto = mapper.ProjectTo<ProductQueryDto>((response.Data).AsQueryable());

            var result = GeneralResponse<IEnumerable<ProductQueryDto>>.SuccessResponse(dto, response.Message);
            return result;
        }

        // ✏️ Update
        public async Task<GeneralResponse<Product>> UpdateProductAsync(ProductCommendDto dto)
        {
            var Product = mapper.Map<Product>(dto);

            var response = await _baseRepo.UpdateAsync(Product);

            if (!response.Success)
                return GeneralResponse<Product>.FailResponse("Updating the Product is fail");

            await unitOfWork.SaveChangesAsync();

            return response;


        }

        // ❌ Delete
        public async Task<GeneralResponse<Product>> DeleteProductAsync(int id)
        {
            var response = await _baseRepo.DeleteAsync(id);

            if (!response.Success)
                return GeneralResponse<Product>.FailResponse("Deleting the Product is fail");

            await unitOfWork.SaveChangesAsync();
            return response;
        }

    }
}
