using Microsoft.EntityFrameworkCore;
using MovieAPI_Project.Entity;
using MovieAPI_Project.Exceptions;

namespace MovieAPI_Project
{
    public class SqlDataRepository:IDataRepository
    {
        private readonly MovieDbContext _context;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public SqlDataRepository(MovieDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Adding a character in database.
        /// </summary>
        /// <param name="character"></param>
        public int AddCharacter(Character character)
        {
            _context.Characters.Add(character);
            _context.SaveChanges();
            return character.Id;
        }
        /// <summary>
        /// Adding a franchise in database.
        /// </summary>
        /// <param name="franchise"></param>
        public int AddFranchise(Franchise franchise)
        {
            _context.Franchises.Add(franchise);
            _context.SaveChanges();
            return franchise.Id;
        }
        /// <summary>
        /// Adding a movie in database.
        /// </summary>
        /// <param name="movie"></param>
        public int AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return movie.Id;
        }
        /// <summary>
        /// Gets all the characters from the database
        /// </summary>
        /// <returns></returns>
        public List<Character> GetCharacters() => _context.Characters.ToList();
        /// <summary>
        /// Deletes a character from the database by name
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCharacter(int id)
        {
            var character = _context.Characters.FirstOrDefault(c => c.Id == id);
            if (character == null)
                throw new NotFoundException($"Character not found");

            _context.Characters.Remove(character);
            _context.SaveChanges();
        }
        /// <summary>
        /// Deletes a franchise from the database by name
        /// </summary>
        /// <param name="id"></param>
        public void DeleteFranchise(int id)
        {
            var franchise = _context.Franchises.FirstOrDefault(f => f.Id == id);

            if (franchise == null)
                throw new NotFoundException("This franchise is not found in the system!");

            _context.Franchises.Remove(franchise);
            _context.SaveChanges();
        }
        /// <summary>
        /// Deletes a movie from the database by id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                throw new NotFoundException("This movie is not found in the system!");

            _context.Movies.Remove(movie);
            _context.SaveChanges();

        }
        /// <summary>
        /// Gets a character by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Character GetCharacter(int id)
        {
            return _context.Characters.FirstOrDefault(c => c.Id == id);
        }
        /// <summary>
        /// Gets a franchise by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Franchise GetFranchise(int id)
        {
            return _context.Franchises.FirstOrDefault(f => f.Id == id);
        }
        /// <summary>
        /// Gets list of franchises
        /// </summary>
        /// <returns></returns>
        public List<Franchise> GetFranchises()
        {
            return _context.Franchises.ToList();
        }
        /// <summary>
        /// Gets a movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Movie GetMovie(int id)
        {
            return _context.Movies.FirstOrDefault(m => m.Id == id);
        }
        /// <summary>
        /// Gets list of movies
        /// </summary>
        /// <returns></returns>
        public List<Movie> GetMovies()
        {
            return _context.Movies.ToList();

        }
        /// <summary>
        /// Updates a character in the database 
        /// </summary>
        /// <param name="character"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateCharacter(Character character)
        {
            //First check CharacterId:
            var updatedCharacter = _context.Characters.FirstOrDefault(x => x.Id == character.Id);
            if (updatedCharacter == null)
            {
                throw new NotFoundException($"Character not found with this Id {character.Id}!");
            }

            // Updates the character properties:
            updatedCharacter.FullName = character.FullName;
            updatedCharacter.Picture = character.Picture;
            updatedCharacter.Gender = character.Gender;
            updatedCharacter.Alias = character.Alias;

            _context.SaveChanges();

        }
        /// <summary>
        /// Updates a franchise in the database
        /// </summary>
        /// <param name="franchise"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateFranchise(Franchise franchise)
        {
            //First check FranchiseId:
            var UpdatedFranchise = _context.Franchises.FirstOrDefault(x => x.Id == franchise.Id);
            if (UpdatedFranchise == null)
            {
                throw new NotFoundException($"Franchise not found with this Id {franchise.Id}!");
            }

            // Updates the franchise properties:
            UpdatedFranchise.Name = franchise.Name;
            UpdatedFranchise.Description = franchise.Description;
            _context.SaveChanges();

        }
        /// <summary>
        /// Updates a movie in the database
        /// </summary>
        /// <param name="movie"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateMovie(Movie movie)
        {
            //First check MovieId:
            var UpdatedMovie = _context.Movies.FirstOrDefault(x => x.Id == movie.Id);
            if (UpdatedMovie == null)
            {
                throw new NotFoundException($"Movie not found with this Id {movie.Id}!");
            }

            // Updates the movie properties:
            UpdatedMovie.Title = movie.Title;
            UpdatedMovie.Genre = movie.Genre;
            UpdatedMovie.ReleaseYear = movie.ReleaseYear;
            UpdatedMovie.Director = movie.Director;
            UpdatedMovie.Picture = movie.Picture;
            UpdatedMovie.Trailer = movie.Trailer;

            _context.SaveChanges();
        }

        /// <summary>
        /// Updates movie with the characters
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="characterIds"></param>
        /// <exception cref="NotFoundException"></exception>
        public void UpdateMovieCharacters(int movieId, List<int> characterIds)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == movieId);
            if (movie == null)
            {
                throw new NotFoundException($"Movie not found with this Id {movie.Id}!");
            }

            movie.Characters = new List<Character>();
            foreach (var characterId in characterIds)
            {
                var character = GetCharacter(characterId);
                movie.Characters.Add(character);
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Updates franchise with the movies
        /// </summary>
        /// <param name="franchiseId"></param>
        /// <param name="movieIds"></param>
        /// <exception cref="NotFoundException"></exception>
        public void UpdateFranchiseMovies(int franchiseId, List<int> movieIds)
        {
            var franchise = _context.Franchises.FirstOrDefault(x => x.Id == franchiseId);
            if (franchise == null)
            {
                throw new NotFoundException($"Franchise not found with this Id {franchiseId}!");
            }

            franchise.Movies = new List<Movie>();
            foreach (var movieId in movieIds)
            {
                var movie = GetMovie(movieId);

                if (movie == null)
                {
                    throw new NotFoundException($"Movie not found with this Id {movieId}!");
                }

                franchise.Movies.Add(movie);
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Gets movies for the given franchise id
        /// </summary>
        /// <param name="franchiseId"></param>
        /// <returns></returns>
        public List<Movie> GetMovies(int franchiseId)
        {
            return _context.Movies.Where(m => m.FranchiseId == franchiseId).ToList();
        }

        /// <summary>
        /// Gets all the characters in a franchise
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public List<Character> GetMovieCharacters(int movieId)
        {
            var movie = _context.Movies.Include(movie => movie.Characters).FirstOrDefault(movie => movie.Id == movieId);
            if (movie == null)
            {
                throw new NotFoundException($"Movie not found with this Id {movie.Id}!");
            }

            return movie.Characters;
        }

        /// <summary>
        /// Gets all the characters in a movie
        /// </summary>
        /// <param name="franchiseId"></param>
        /// <returns></returns>
        public List<Character> GetFranchiseCharacters(int franchiseId)
        {
            //Checking if franchise exists
            var franchise = _context.Franchises.FirstOrDefault(x => x.Id == franchiseId);
            if (franchise == null)
            {
                throw new NotFoundException($"Franchise not found with this Id {franchiseId}!");
            }

            var movies = _context.Movies.Include(movie => movie.Characters).Where(movie => movie.FranchiseId == franchiseId).ToList();

            var characters = new List<Character>();
            foreach (var movie in movies)
            {
                characters.AddRange(movie.Characters);
            }

            return characters;
        }
    }
}
