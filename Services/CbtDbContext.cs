using CyberSpaceCBT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSpaceCBT.Services
{
    public class CbtDbContext : DbContext
    {
        public CbtDbContext(DbContextOptions<CbtDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<TestScore> TestScores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
