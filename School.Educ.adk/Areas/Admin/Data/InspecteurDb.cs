using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Admin.Data
{
    public class InspecteurDb : DbContext
    {
        public InspecteurDb(DbContextOptions<InspecteurDb> options) : base(options) { }

        public DbSet<Inspecteur> Inspecteurs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
