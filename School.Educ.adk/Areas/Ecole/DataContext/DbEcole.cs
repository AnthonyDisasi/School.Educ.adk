using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Ecole.DataContext
{
    public class DbEcole : DbContext
    {
        public DbEcole(DbContextOptions<DbEcole> options) : base(options) { }

        public DbSet<Categorie> categories { get; set; }
        public DbSet<Cours> Cours { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Directeur> Directeurs { get; set; }
        public DbSet<Models.Ecole> Ecoles { get; set; }
        public DbSet<Eleve> Eleves { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Models.Professeur> Professeurs { get; set; }
        public DbSet<SousDivision> SousDivisions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
