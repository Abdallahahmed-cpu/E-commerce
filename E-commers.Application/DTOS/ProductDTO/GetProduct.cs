using E_commers.Application.DTOS.CategoryDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commers.Application.DTOS.ProductDTO
{
    public class GetProduct : ProductBase
    {
      
        public Guid Id { get; set; }
        public GetCategory? category { get; set; }
    }
}