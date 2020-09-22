using LeilaoWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeilaoWeb.Services
{
    public class OfferService
    {
        // dependencia
        private readonly LeilaoWebContext _context;
        public OfferService(LeilaoWebContext context)
        {
            _context = context;
        }

        public async Task<List<Offer>> FindAllAsync()
        {
            return await _context.Offer.OrderBy(x => x.OfferId).ToListAsync();
        }


        public async Task<List<Offer>> FindAllByPeopleIdAsync(int id)
        {
            var result = from obj in _context.Offer select obj;
             result= result.Where(x => x.PeopleId == id);

            return await result
                .Include(x => x.People)
                .Include(x => x.Product)
                .OrderByDescending(x => x.PeopleId)
                .ToListAsync();
        }

        public async Task<List<Offer>> FindAllByProductIdAsync()
        {
            var result = from obj in _context.Offer select obj;
             result = result.OrderBy(x => x.ProductId);

            return await result
               .Include(x => x.People)
               .Include(x => x.Product)
               .OrderByDescending(x => x.PeopleId)
               .ToListAsync();
        }


        public async Task<List<Offer>> FindAllByProductIdAsync(int id)
        {
            return await _context.Offer.Where(x => x.ProductId == id).ToListAsync();
        }


        public async Task<decimal> FindBigValueByProductIdAsync(int id)
        {
            decimal value = 0;
            var prod = await FindAllByProductIdAsync(id);

            if (prod != null && prod.Count > 0)
            {
                value = prod.Max(x => x.Value);
            }

            return value;
        }

        public async Task InsertAsync(Offer obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
    }
}
