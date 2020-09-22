using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeilaoWeb.Models
{
    public class Offer
    {

        public int OfferId { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(0.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Value Offer")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        public decimal Value { get; set; }


        [Required(ErrorMessage = "{0} required")]
        public int PeopleId { get; set; }


        [Required(ErrorMessage = "{0} required")]
        public int ProductId { get; set; }

        public People People { get; set; }
        public Product Product { get; set; }


        public Offer()
        {
        }

        public Offer(int offerId, decimal value, People people, Product product)
        {
            OfferId = offerId;
            Value = value;
            People = people;
            Product = product;
        }
    }
}
