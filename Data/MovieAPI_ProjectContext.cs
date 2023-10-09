using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAPI_Project.Entity;

namespace MovieAPI_Project.Data
{
    public class MovieAPI_ProjectContext : DbContext
    {
        public MovieAPI_ProjectContext (DbContextOptions<MovieAPI_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<MovieAPI_Project.Entity.Movie> Movie { get; set; } = default!;
    }
}
