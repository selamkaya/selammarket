using Microsoft.EntityFrameworkCore;
using SelamMarket.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelamMarket.Repository
{
    public class SelamMarketDbContext : DbContext
    {
        //public SelamMarketDbContext()
        //{
           
        //}

        public SelamMarketDbContext(DbContextOptions<SelamMarketDbContext> dbContextOptions)
       : base(dbContextOptions)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Order> Orders { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Logistics;Trusted_Connection=True;");

        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
