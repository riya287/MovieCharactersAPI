using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI_Project;
using MovieAPI_Project.DTOs;
using MovieAPI_Project.Entity;
using MovieAPI_Project.Exceptions;
using NuGet.Protocol.Core.Types;

namespace MovieAPI_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("Application/json")] //for documenting the media type
    [Consumes("Application/json")]
    public class MovieController : ControllerBase
    {
        private readonly IDataRepository _repository;
        private readonly IMapper _mapper;

        public MovieController(IDataRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets all the movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieDTO>))]
        public async Task<IActionResult> GetMovies()
        {
            var movies = _repository.GetMovies();
            var moviesDTO = _mapper.Map<List<MovieDTO>>(movies);
            return Ok(moviesDTO);
        }
        /// <summary>
        /// Gets a movie with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundDTO))]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = _repository.GetMovie(id);
            if (movie == null)
                return NotFound(new NotFoundDTO { Message = $"Movie not found with Id {id}" });
            var movieDTO = _mapper.Map<MovieDTO>(movie);
            return Ok(movieDTO);
        }
        /// <summary>
        /// Adds a new movie
        /// </summary>
        /// <param name="movieDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> AddMovie([FromBody] MovieDTO movieDTO)
        {
            try
            {
                var movie = _mapper.Map<Movie>(movieDTO);
                var movieId = _repository.AddMovie(movie);
                var dto = new SuccessDTO
                {
                    Message = "Movie added successfully",
                    Id = movieId
                };
                return Ok(dto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorDTO { Message = ex.Message });
            }

        }
        /// <summary>
        /// Updates an existing movie
        /// </summary>
        /// <param name="movieDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieDTO movieDTO)
        {
            try
            {
                var updatedMovie = _mapper.Map<Movie>(movieDTO);
                _repository.UpdateMovie(updatedMovie);
                return Ok(new SuccessDTO { Message = "Movie updated successfully.", Id = movieDTO.Id });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundDTO { Message = $"Movie not found with Id {movieDTO.Id}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorDTO { Message = ex.Message });
            }

        }
        /// <summary>
        /// Deletes a movie with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                _repository.DeleteMovie(id);
                return Ok(new SuccessDTO { Message = "Movie deleted successfully.", Id = id });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundDTO { Message = $"Movie not found with Id {id}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorDTO { Message = ex.Message });
            }
        }
        /// <summary>
        /// Gets all the characters in a movie with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> GetMovieCharacters(int id)
        {
            try
            {
                var characters = _repository.GetMovieCharacters(id);
                var charactersDTO = _mapper.Map<List<CharacterDTO>>(characters);
                return Ok(charactersDTO);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundDTO { Message = $"Movie not found with Id {id}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorDTO { Message = ex.Message });
            }

        }
    }
}
