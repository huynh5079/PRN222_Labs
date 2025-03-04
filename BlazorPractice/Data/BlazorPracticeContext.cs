using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorPractice.Models;

namespace BlazorPractice.Data
{
    public class BlazorPracticeContext : DbContext
    {
        public BlazorPracticeContext (DbContextOptions<BlazorPracticeContext> options)
            : base(options)
        {
        }

        public DbSet<BlazorPractice.Models.Movie> Movie { get; set; } = default!;
    }
}
