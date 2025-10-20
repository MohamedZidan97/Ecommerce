using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zain.Application.Contracts;
using Zain.Application.Features;
using Zain.Application.IUnit;
using Zain.Application.Servcies.Interface;
using Zain.Domain.Entities;

namespace Zain.Application.Servcies.Repository
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IBaseRepository<Customer> _baseRepo;
        private readonly IUnitOfWork unitOfWork;

        public CustomerServices(IBaseRepository<Customer> baseRepo,IUnitOfWork unitOfWork)
        {
            _baseRepo = baseRepo;
            this.unitOfWork = unitOfWork;
        }

        // ➕ Add
        public async Task<GeneralResponse<Customer>> AddCustomerAsync(Customer customer)
        {
            var response = await _baseRepo.AddAsync(customer);

            if (!response.Success)
                return GeneralResponse<Customer>.FailResponse("Adding the customer is fail");

           await unitOfWork.SaveChangesAsync();

            return response;
        }

        // 🔍 GetById
        public async Task<GeneralResponse<Customer>> GetCustomerByIdAsync(int id)
        {
            var response = await _baseRepo.GetByIdAsync(id);
            if (!response.Success)
                return GeneralResponse<Customer>.FailResponse("Customer not found");

            return response;
        }

        // 📋 GetAll
        public async Task<GeneralResponse<IEnumerable<Customer>>> GetAllCustomersAsync()
        {
            var response = await _baseRepo.GetAllAsync();

         
            return response;
        }

        // ✏️ Update
        public async Task<GeneralResponse<Customer>> UpdateCustomerAsync(Customer customer)
        {
            var response = await _baseRepo.UpdateAsync(customer);

            await unitOfWork.SaveChangesAsync();

            return response;


        }

        // ❌ Delete
        public async Task<GeneralResponse<Customer>> DeleteCustomerAsync(int id)
        {
            var response = await _baseRepo.DeleteAsync(id);

            await unitOfWork.SaveChangesAsync();

            return response;
        }



    }
}
