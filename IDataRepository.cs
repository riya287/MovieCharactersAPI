using MovieAPI_Project.Entity;

namespace MovieAPI_Project
{
    public interface IDataRepository
    {
        /// <summary>
        /// Returns all movies
        /// </summary>
        /// <returns></returns>
        List<Movie> GetMovies();

        /// <summary>
        /// Returns movie by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Movie GetMovie(int id);

        /// <summary>
        /// Adds a new movie
        /// </summary>
        /// <param name="movie"></param>
        int AddMovie(Movie movie);

        /// <summary>
        /// Updates an existing movie 
        /// </summary>
        /// <param name="movie"></param>
        void UpdateMovie(Movie movie);

        /// <summary>
        /// Deletes a movie by Id
        /// </summary>
        /// <param name="id"></param>
        void DeleteMovie(int id);

        /// <summary>
        /// Returns all the characters
        /// </summary>
        /// <returns></returns>
        List<Character> GetCharacters();

        /// <summary>
        /// Return a charcter by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Character GetCharacter(int id);
        /// <summary>
        /// Adds a new character in the list
        /// </summary>
        /// <param name="character"></param>
        int AddCharacter(Character character);
        /// <summary>
        /// Updates a character
        /// </summary>
        /// <param name="character"></param>
        void UpdateCharacter(Character character);
        /// <summary>
        /// deletes a character from the list
        /// </summary>
        /// <param name="character"></param>
        void DeleteCharacter(int id);
        /// <summary>
        /// Returns list of the franchises
        /// </summary>
        /// <returns></returns>
        List<Franchise> GetFranchises();
        /// <summary>
        /// Returns a franchise from the list by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Franchise GetFranchise(int id);

        /// <summary>
        /// Adds a Franchise in the list
        /// </summary>
        /// <param name="franchise"></param>
        int AddFranchise(Franchise franchise);
        /// <summary>
        /// Updates a franchise
        /// </summary>
        /// <param name="franchise"></param>
        void UpdateFranchise(Franchise franchise);
        /// <summary>
        /// Deletes a franchise from the list
        /// </summary>
        /// <param name="franchise"></param>
        void DeleteFranchise(int id);

        /// <summary>
        /// Updates movie with the characters
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="characterIds"></param>
        void UpdateMovieCharacters(int movieId, List<int> characterIds);

        /// <summary>
        /// Updates franchise with the movies
        /// </summary>
        /// <param name="franchiseId"></param>
        /// <param name="movieIds"></param>
        void UpdateFranchiseMovies(int franchiseId, List<int> movieIds);

        /// <summary>
        /// Gets movies for the given franchise id
        /// </summary>
        /// <param name="franchiseId"></param>
        /// <returns></returns>
        List<Movie> GetMovies(int franchiseId);

        /// <summary>
        /// Gets all the characters in a franchise
        /// </summary>
        /// <param name="franchiseId"></param>
        /// <returns></returns>
        List<Character> GetFranchiseCharacters(int franchiseId);

        /// <summary>
        /// Gets all the characters in a movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        List<Character> GetMovieCharacters(int movieId);


    }
}

