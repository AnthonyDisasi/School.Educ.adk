using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Educ.adk.Areas.Inspection.Models;

namespace School.Educ.adk.Areas.Inspection.Data
{
    public class ExamenDb : DbContext
    {
        public ExamenDb(DbContextOptions<ExamenDb> options) : base(options) { }

        public DbSet<Evaluer> Evaluers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
