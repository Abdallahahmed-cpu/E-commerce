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
    public class CategoryService(IGeneric<Category>_categoryInterface,IMapper _mapper) : ICategoryService
    {
        public async Task<ServiceResponse> AddAsync(CreateCategory category)
        {
            var mappedData = _mapper.Map<Category>(category);
            int result = await _categoryInterface.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "DONE")
         : new ServiceResponse(false, "Category failed to be deleted");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await _categoryInterface.DeleteAsync(id);


            return result > 0 ? new ServiceResponse(true, "Category Deleted")
           : new ServiceResponse(false, "Category failed to be deleted");
        }
        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var rawData = await _categoryInterface.GetAllAsync();

            // If there is no data, return an empty collection
            if (!rawData.Any())
                return Enumerable.Empty<GetCategory>();

            // Map raw data to the desired output type
            return _mapper.Map<IEnumerable<GetCategory>>(rawData);
        }


        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var rawData = await _categoryInterface.GetByIdAsync(id);

            if (rawData == null) return new GetCategory();

            return _mapper.Map<GetCategory>(rawData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory category)
        {
            var mappedData = _mapper.Map<Category>(category);
            int result = await _categoryInterface.UpdateAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Category Updated")
         : new ServiceResponse(false, "Category failed to be Updated");
        }
    }


  
       
    
}
