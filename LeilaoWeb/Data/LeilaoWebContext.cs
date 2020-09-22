using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LeilaoWeb.Models;

namespace LeilaoWeb.Models
{
    public class LeilaoWebContext : DbContext
    {
        public LeilaoWebContext (DbContextOptions<LeilaoWebContext> options)
            : base(options)
        {
        }

        public DbSet<LeilaoWeb.Models.Product> Product { get; set; }
        public DbSet<LeilaoWeb.Models.People> People { get; set; }
        public DbSet<LeilaoWeb.Models.Offer> Offer { get; set; }
    }
}
