using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commers.Application.DTOS.ProductDTO
{
    public class ProductBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public string? Base64Image { get; set; }
        [Required]
        public string Quantity { get; set; }
        [Required]
        public Guid? categoryId { get; set; }

    } 
}
