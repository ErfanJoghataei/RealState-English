using Microsoft.EntityFrameworkCore;
using RealState.Dal.Entities;
using RealState.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealState.Dal.Contexs
{
    public class RealStateDbContext:DbContext
    {
        public RealStateDbContext(DbContextOptions<RealStateDbContext> options) : base(options)
        {
        }



        public DbSet<Admin> Admins   { get; set; }
        public DbSet<Properties>  properties { get; set; }           
        public DbSet<User> Users { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }



    }
}
