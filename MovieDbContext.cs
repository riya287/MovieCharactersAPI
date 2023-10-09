using System.Data;
using Microsoft.EntityFrameworkCore;
using MovieAPI_Project.Entity;

namespace MovieAPI_Project
{
    public class MovieDbContext : DbContext
    {
        //Constructor- options
        public MovieDbContext(DbContextOptions options) : base(options)
        {
        }

        //Pysical tables
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 1,
                Title = "Titanic",
                Genre = "Romance",
                ReleaseYear = 1997,
                Director = "James Cameron",
                Picture = "https://deadline.com/2022/06/titanic-rerelease-date-remastered-version-james-cameron-1235050212/",
                Trailer = "https://www.youtube.com/watch?v=kVrqfYjkTdQ",
                FranchiseId = 1
            },
            new Movie
            {
                Id = 2,
                Title = "Superman",
                Genre = "Action",
                ReleaseYear = 1979,
                Director = "Richard Lester",
                Picture = "https://www.rottentomatoes.com/m/superman_the_movie",
                Trailer = "https://www.youtube.com/watch?v=pUwxH4SM9Rg",
                FranchiseId = 1
            },
            new Movie
            {
                Id = 3,
                Title = "X-Men",
                Genre = "Fiction",
                ReleaseYear = 2000,
                Director = "James Mangold",
                Picture = "https://www.imdb.com/title/tt3385516/",
                Trailer = "https://www.youtube.com/watch?v=VNxwlx6etXI",
                FranchiseId = 2
            });
            modelBuilder.Entity<Character>().HasData(new Character
            {
                Id = 1,
                FullName = "Leonardo DiCaprio",
                Gender = "Male",
                Picture = "https://img.washingtonpost.com/rf/image_1484w/2010-2019/WashingtonPost/2012/03/28/Style/Images/t3282.jpg?uuid=_HqPaHjaEeGW_E4_moVh7g",

            },
            new Character
            {
                Id = 2,
                FullName = "Clark Joseph Kent",
                Gender = "Male",
                Picture = "https://upload.wikimedia.org/wikipedia/en/d/d6/Superman_Man_of_Steel.jpg",

            },
            new Character
            {
                Id = 3,
                FullName = "Henry Philip",
                Gender = "Male",
                Picture = "https://upload.wikimedia.org/wikipedia/en/0/08/X-Men_Origins_Wolverine_theatrical_poster.jpg",

            });
            modelBuilder.Entity<Franchise>().HasData(new Franchise
            {
                Id = 1,
                Name = "Marvel Movies",
                Description = "This franchise makes superhit movies.",

            },
            new Franchise
            {
                Id = 2,
                Name = "Mad max disaster",
                Description = "This franchise makes superhit movies.",

            },
             new Franchise
             {
                 Id = 3,
                 Name = "Avengers Infinity War",
                 Description = "This franchise makes superhit movies.",

             });

        }

    }
}
