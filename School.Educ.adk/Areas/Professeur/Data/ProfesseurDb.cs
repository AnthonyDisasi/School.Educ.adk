using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Professeur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Professeur.Data
{
    public class ProfesseurDb : DbContext
    {
        public ProfesseurDb(DbContextOptions<ProfesseurDb> options) : base(options) { }

        public DbSet<Cotation> Cotations { get; set; }
        public DbSet<Echange> Echanges { get; set; }
        public DbSet<Epreuve> Epreuves { get; set; }
        public DbSet<Lecon> Lecons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
