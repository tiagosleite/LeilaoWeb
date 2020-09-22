using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LeilaoWeb.Models
{
    public class Product
    {

        public int ProductId { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Nome do Produto")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Value is required.")]        
        [Range(0.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Value Offer")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        public decimal Value { get; set; }

        public Product()
        {           
        }

        public Product(int productId, string name, decimal value)
        {
            ProductId = productId;
            Name = name;
            Value = value;
        }

        public bool CheckValue(decimal newValue)
        {
            if (newValue > Value)
            {
                return true;
            }
            return false;
        }
    }
}
