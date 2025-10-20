using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zain.Application.IUnit;
using Zain.Application.Servcies.Interface;
using Zain.Domain.Entities;

namespace Zain.Controllers.Client
{
    [Route("api/client/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerService;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(ICustomerServices customerService, IUnitOfWork unitOfWork)
        {
            _customerService = customerService;
            _unitOfWork = unitOfWork;
        }

        // GET: api/customer
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        // GET: api/customer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer.Success)
                return BadRequest(customer);

            return Ok(customer);
        }

        // POST: api/customer
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res =  await _customerService.AddCustomerAsync(customer);

            if (!res.Success)
                return BadRequest(res);

            return Created("Done",res);
        }

        // PUT: api/customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
                return BadRequest("Mismatched customer ID.");

            var existing = await _customerService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _customerService.UpdateAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _customerService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _customerService.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
