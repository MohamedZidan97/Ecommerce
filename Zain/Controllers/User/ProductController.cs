using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zain.Application.IUnit;
using Zain.Application.Servcies.Interface;
using Zain.Domain.Entities;

namespace Zain.Controllers.User
{
    //[Route("api/user/[controller]")]
 //   [ApiController]
//    public class ProductController : ControllerBase

//    {
//        private readonly IProductServices _ProductService;
//    private readonly IUnitOfWork _unitOfWork;

//    public ProductController(IProductServices ProductService, IUnitOfWork unitOfWork)
//    {
//        _ProductService = ProductService;
//        _unitOfWork = unitOfWork;
//    }

//    // GET: api/Product
//    [HttpGet]
//    public async Task<IActionResult> GetAll()
//    {
//        var Products = await _ProductService.GetAllProductsAsync();
//        return Ok(Products);
//    }

//    // GET: api/Product/5
//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetById(int id)
//    {
//        var Product = await _ProductService.GetProductByIdAsync(id);
//        if (Product.Success)
//            return BadRequest(Product);

//        return Ok(Product);
//    }

//    // POST: api/Product
//    [HttpPost]
//    public async Task<IActionResult> Create([FromBody] Product Product)
//    {
//        if (!ModelState.IsValid)
//            return BadRequest(ModelState);

//        var res = await _ProductService.AddProductAsync(Product);

//        if (!res.Success)
//            return BadRequest(res);

//        return Created("Done", res);
//    }

//    // PUT: api/Product/5
//    [HttpPut("{id}")]
//    public async Task<IActionResult> Update(int id, [FromBody] Product Product)
//    {
//        if (id != Product.Id)
//            return BadRequest("Mismatched Product ID.");

//        var existing = await _ProductService.GetByIdAsync(id);
//        if (existing == null)
//            return NotFound();

//        await _ProductService.UpdateAsync(Product);
//        await _unitOfWork.SaveChangesAsync();

//        return NoContent();
//    }

//    // DELETE: api/Product/5
//    [HttpDelete("{id}")]
//    public async Task<IActionResult> Delete(int id)
//    {
//        var existing = await _ProductService.GetByIdAsync(id);
//        if (existing == null)
//            return NotFound();

//        await _ProductService.DeleteAsync(id);
//        await _unitOfWork.SaveChangesAsync();

//        return NoContent();
//    }
//}
}
