using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RealState.Dal.Contexs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealState.Dal.Factory
{
    public class RealStateDbContextFactory:IDesignTimeDbContextFactory<RealStateDbContext>
    {
        public RealStateDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RealStateDbContext>();

            // Keep the English copy on its own local development database.
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RealStateEnglishSiteDb;Trusted_Connection=True;TrustServerCertificate=True");

            return new RealStateDbContext(optionsBuilder.Options);
        }
    }
    
    
}

