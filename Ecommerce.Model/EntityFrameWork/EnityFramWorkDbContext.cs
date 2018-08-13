using Ecommerce.Model.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Model.EntityFrameWork
{
    public partial class EnityFramWorkDbContext : DbContext
    {
        public EnityFramWorkDbContext(DbContextOptions<EnityFramWorkDbContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<BasketItem> BasketItems { get; set; }
    }
}
