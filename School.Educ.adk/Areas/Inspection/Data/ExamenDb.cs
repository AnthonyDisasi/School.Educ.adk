using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Inspection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Inspection.Data
{
    public class ExamenDb : DbContext
    {
        public ExamenDb(DbContextOptions<ExamenDb> options) : base(options) { }

        public DbSet<Assertion> Assertions { get; set; }
        public DbSet<Examen> Examens { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Reponse> Reponses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
