using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zain.Domain.Entities;

namespace Zain.Application.Features.Users
{
    public class ProductDto
    {

        public class ProductQueryDto
        {
            public int ProductId { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
            public int Quentity { get; set; }

            [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
            public decimal UnitPrice { get; set; }

            [Required(ErrorMessage = "Name is required")]
            [StringLength(100, ErrorMessage = "Name must not exceed 100 characters")]
            public string Name { get; set; }

            [StringLength(500, ErrorMessage = "Description must not exceed 500 characters")]
            public string Description { get; set; }

            [Required(ErrorMessage = "SubCategoryId is required")]
            public int SubCategoryId { get; set; }

            public SubCategory SubCategory { get; set; }
        }

        public class ProductFormDto
        {
            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
            public int Quentity { get; set; }

            [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
            public decimal UnitPrice { get; set; }

            [Required(ErrorMessage = "Name is required")]
            [StringLength(100, ErrorMessage = "Name must not exceed 100 characters")]
            public string Name { get; set; }

            [StringLength(500, ErrorMessage = "Description must not exceed 500 characters")]
            public string Description { get; set; }

            [Required(ErrorMessage = "SubCategoryId is required")]
            public int SubCategoryId { get; set; }
        }

        public class ProductCommendDto
        {
            [Required(ErrorMessage = "ProductId is required")]
            public int ProductId { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
            public int Quentity { get; set; }

            [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
            public decimal UnitPrice { get; set; }

            [Required(ErrorMessage = "Name is required")]
            [StringLength(100, ErrorMessage = "Name must not exceed 100 characters")]
            public string Name { get; set; }

            [StringLength(500, ErrorMessage = "Description must not exceed 500 characters")]
            public string Description { get; set; }

            [Required(ErrorMessage = "SubCategoryId is required")]
            public int SubCategoryId { get; set; }
        }
    }
}
