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

namespace MovieAPI_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("Application/json")] //for documenting the media type
    [Consumes("Application/json")]
    public class FranchiseController : ControllerBase
    {
        private readonly IDataRepository _repository;
        private readonly IMapper _mapper;

        public FranchiseController(IDataRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets all the franchises
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FranchiseDTO>))]
        public async Task<IActionResult> GetFranchises()
        {
            var franchises = _repository.GetFranchises();
            var franchisesDTO = _mapper.Map<List<FranchiseDTO>>(franchises);
            return Ok(franchisesDTO);
        }
        /// <summary>
        /// Gets a franchise with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FranchiseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundDTO))]
        public async Task<IActionResult> GetFranchise(int id)
        {
            var franchise = _repository.GetFranchise(id);
            if (franchise == null)
                return NotFound(new NotFoundDTO { Message = $"Franchise not found with Id {id}" });

            var franchiseDTO = _mapper.Map<FranchiseDTO>(franchise);
            return Ok(franchiseDTO);
        }
        /// <summary>
        /// Adds a new franchise
        /// </summary>
        /// <param name="franchiseDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> AddFranchise([FromBody] FranchiseDTO franchiseDTO)
        {
            try
            {
                var franchise = _mapper.Map<Franchise>(franchiseDTO);
                var franchiseId = _repository.AddFranchise(franchise);
                var dto = new SuccessDTO
                {
                    Message = "Franchise added successfully",
                    Id = franchiseId
                };
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorDTO { Message = ex.Message });
            }

        }
        /// <summary>
        /// Updates a franchise
        /// </summary>
        /// <param name="franchiseDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> UpdateFranchise([FromBody] FranchiseDTO franchiseDTO)
        {
            try
            {
                var updatedFranchise = _mapper.Map<Franchise>(franchiseDTO);
                _repository.UpdateFranchise(updatedFranchise);
                return Ok(new SuccessDTO { Message = "Franchise updated successfully.", Id = franchiseDTO.Id });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundDTO { Message = $"Franchise not found with Id {franchiseDTO.Id}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorDTO { Message = ex.Message });
            }

        }
        /// <summary>
        /// Deletes an existing franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundDTO))]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            try
            {
                _repository.DeleteFranchise(id);
                return Ok(new SuccessDTO { Message = "Franchise deleted successfully.", Id = id });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundDTO { Message = $"Franchise not found with Id {id}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorDTO { Message = ex.Message });
            }
        }
        /// <summary>
        /// Updates a movie in a given franchise by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateMoviesDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> UpdateFranchiseMovies(int id, [FromBody] UpdateMoviesDTO updateMoviesDTO)
        {
            try
            {
                _repository.UpdateFranchiseMovies(id, updateMoviesDTO.MovieIds);
                return Ok(new SuccessDTO { Message = "Movies in the franchise updated successfully.", Id = id });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundDTO { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorDTO { Message = ex.Message });
            }
        }
        /// <summary>
        /// Gets all the movies in a franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/movies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundDTO))]
        public async Task<IActionResult> GetFranchiseMovies(int id)
        {
            try
            {
                var franchise = _repository.GetFranchise(id);

                if (franchise == null)
                    return NotFound(new NotFoundDTO { Message = $"Franchise not found with Id {id}" });

                var movies = _repository.GetMovies(id);
                var moviesDTO = _mapper.Map<List<MovieDTO>>(movies);

                return Ok(moviesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorDTO { Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets all the characters in a franchise 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CharacterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundDTO))]
        public async Task<IActionResult> GetFranchiseCharacters(int id)
        {
            try
            {
                var franchise = _repository.GetFranchise(id);

                if (franchise == null)
                    return NotFound(new NotFoundDTO { Message = $"Franchise not found with Id {id}" });

                var movies = _repository.GetFranchiseCharacters(id);
                var charDTO = _mapper.Map<List<CharacterDTO>>(movies);

                return Ok(charDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorDTO { Message = ex.Message });
            }
        }
    }
}
