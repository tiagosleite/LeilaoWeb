using LeilaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LeilaoWeb.Services
{
    public class PeopleService
    {
        private readonly LeilaoWebContext _context;
        public PeopleService(LeilaoWebContext context)
        {
            _context = context;
        }

        public async Task<List<People>> FindAllAsync()
        {
            return await _context.People.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<int> FindByNameAsync(string name)
        {
            People people = null;
            people= await _context.People.FirstOrDefaultAsync(x => x.Name == name);

            if(people != null)
            {
                return  people.PeopleId;
            }

            return -1;
        }
    }
}
