using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.ProfeArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.ProfeArea.Data
{
    public class ProfeAreaDb : DbContext
    {
        public ProfeAreaDb(DbContextOptions<ProfeAreaDb> options) : base(options) { }

        public DbSet<CahierCote> CahierCotes { get; set; }
        public DbSet<Cotation> Cotations { get; set; }
        public DbSet<Epreuve> Epreuves { get; set; }
        public DbSet<Lecon> Lecons { get; set; }
        public DbSet<Evaluer> Evaluers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
