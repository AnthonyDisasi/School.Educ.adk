using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.ProfeArea.Data
{
    public class ProfeAreaDb : DbContext
    {
        public ProfeAreaDb(DbContextOptions<ProfeAreaDb> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
