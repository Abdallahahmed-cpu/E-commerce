using AutoMapper;
using E_commers.Application.DTOS;
using E_commers.Application.DTOS.CategoryDTO;
using E_commers.Application.DTOS.ProductDTO;
using E_commers.Application.Service.Interfaces;
using E_commers.Domain.Interface;
using E_commers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commers.Application.Service.Implementations
{
    public class ProductSevice(IGeneric<Product> _productInterface,IMapper _mapper) : IProductService
    {
        public async Task<ServiceResponse> AddAsync(CreateProduct product)
        {
            var mappedData = _mapper.Map<Product>(product);
            int result = await _productInterface.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Product added")
         : new ServiceResponse(false, "Product failed to be added");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
         int result =  await _productInterface.DeleteAsync(id);
           
            return result > 0 ? new ServiceResponse(true, "Product Deleted") 
           :new ServiceResponse(false, "Product failed to be deleted");
        }

       
        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            var rawData = await _productInterface.GetAllAsync();

            // If there is no data, return an empty collection
            if (!rawData.Any())
                return Enumerable.Empty<GetProduct>();

            // Map raw data to the desired output type
            return _mapper.Map<IEnumerable<GetProduct>>(rawData);
        }
        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
            var rawData = await _productInterface.GetByIdAsync(id);

            if (rawData==null) return new GetProduct();

            return _mapper.Map<GetProduct>(rawData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProduct product)
        {
            var mappedData = _mapper.Map<Product>(product);
            int result = await _productInterface.UpdateAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Product Updated")
         : new ServiceResponse(false, "Product failed to be Updated");
        }
    }
}
