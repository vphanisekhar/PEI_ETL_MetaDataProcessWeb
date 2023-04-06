﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PEI_ETL.Core.Entities;
using UnitOfWorkDemo.Core.Models;

namespace PEI_ETL.Infrastrucure
{

    public class ETLDbContext : DbContext //IdentityDbContext<IdentityUser>
    {
        public ETLDbContext(DbContextOptions<ETLDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<ProductDetails> Products { get; set; }
    }
}