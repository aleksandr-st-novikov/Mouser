using Mouser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouser.Domain
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<PriceBreak> PriceBreaks { get; set; }
        public DbSet<AlternatePackaging> AlternatePackagings { get; set; }
        public DbSet<ProductCompliance> ProductCompliances { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Proxy> Proxies { get; set; }
        public DbSet<ApiSearchSession> ApiSearchSessions { get; set; }
        public DbSet<ApiRegInfo> ApiRegInfos { get; set; }

        public ApplicationContext() : base("DBConnection")
        { }
    }
}
