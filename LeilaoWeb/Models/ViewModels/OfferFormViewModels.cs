using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeilaoWeb.Models.ViewModels
{
    public class OfferFormViewModels
    {
        public Offer Offer { get; set; }

        public ICollection<People> Peoples { get; set; }
        public ICollection<Product> Products { get; set; }        
    }
}
