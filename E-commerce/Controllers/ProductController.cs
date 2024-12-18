using AutoMapper;
using E_commers.Application.DTOS;
using E_commers.Application.DTOS.ProductDTO;
using E_commers.Application.Service.Interfaces;
using E_commers.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _productService,IMapper _mapper, ICategoryService _categoryService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _productService.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }

        [HttpGet("Single/{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var data = await _productService.GetByIdAsync(id);
            return data != null ? Ok(data) : NotFound(data);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateProduct product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _productService.AddAsync(product);
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateProduct product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _productService.UpdateAsync(product);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.DeleteAsync(id);
            return result.Success? Ok(result) : BadRequest(result);
        }

    }
}
