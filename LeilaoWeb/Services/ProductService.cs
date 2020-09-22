using LeilaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LeilaoWeb.Services
{
    public class ProductService
    {
        private readonly LeilaoWebContext _context;
        public ProductService(LeilaoWebContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> FindAllAsync()
        {
            return await _context.Product.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            Product product = null;
            product= await _context.Product.FirstOrDefaultAsync(x => x.ProductId == id);
            return product;
        }        
    }
}
