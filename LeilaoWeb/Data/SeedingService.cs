using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeilaoWeb.Models;

namespace LeilaoWeb.Data
{
    public class SeedingService
    {
        private LeilaoWebContext _context;
        public SeedingService(LeilaoWebContext context)
        {
            _context = context;
        }
               
        public void Seed()
        {
            if (_context.Product.Any() || _context.People.Any())
            {
                return;
            }
            else
            {
                // popular banco de dados
                // Product
                Product pr1 = new Product { Name = "Computador", Value = 1000M };
                Product pr2 = new Product { Name = "Celular", Value = 500M };
                Product pr3 = new Product { Name = "Notebook", Value = 900M };
                Product pr4 = new Product { Name = "Ipad", Value = 1500M };

                // People
                People pl1 = new People { Name = "João", Idade = 30 };
                People pl2 = new People { Name = "Maria", Idade = 33 };

                _context.Product.AddRange(pr1, pr2, pr3, pr4);
                _context.People.AddRange(pl1, pl2);
                _context.SaveChangesAsync();
            }
        }
    }
}
