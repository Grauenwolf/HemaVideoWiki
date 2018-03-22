using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HemaVideoWiki.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HemaVideoWiki.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //builder.Entity<ApplicationUser>().Property("UserKey").UseSqlServerIdentityColumn();


            builder.Entity<ApplicationUser>().Property(p => p.UserKey)
               .UseSqlServerIdentityColumn();
            builder.Entity<ApplicationUser>().Property(p => p.UserKey)
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
        }
    }
}
