﻿using Domain.Common;
using Domain.Entity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BloggerContext : IdentityDbContext<ApplicationUser>
    {
        public BloggerContext (DbContextOptions<BloggerContext> options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }

        public async  Task<int> SaveChangesAsync()
        {
            var entries = ChangeTracker.
                Entries().
                Where(e => e.Entity is AuditableEntity && (e.State == EntityState.Modified || e.State == EntityState.Added));

            foreach (var entityEntry in entries)
            {
                ((AuditableEntity)entityEntry.Entity).LastModified = DateTime.UtcNow;

                if(entityEntry.State == EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
            }
            return await base.SaveChangesAsync();
        }
    }
}
