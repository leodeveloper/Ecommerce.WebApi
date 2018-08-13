using Ecommerce.Model.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Model.EntityFrameWork
{
    public class EnityFramWorkDbContext : DbContext
    {
        public EnityFramWorkDbContext(DbContextOptions<EnityFramWorkDbContext> options)
           : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }
    }
}
