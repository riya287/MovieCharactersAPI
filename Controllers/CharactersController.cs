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
    public class CharactersController : ControllerBase
    {
        private readonly IDataRepository _repository;
        private readonly IMapper _mapper;

        public CharactersController(IDataRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the characters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CharacterDTO>))]
        public async Task<IActionResult> GetCharacters()
        {
            var characters = _repository.GetCharacters();
            var charactersDTO = _mapper.Map<List<CharacterDTO>>(characters);
            return Ok(charactersDTO);
        }

        /// <summary>
        /// Gets a character with the given Id
        /// </summary>
        /// <param name="id">Character Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CharacterDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundDTO))]
        public async Task<IActionResult> GetCharacter(int id)
        {
            var character = _repository.GetCharacter(id);
            if (character == null)
                return NotFound(new NotFoundDTO { Message = $"Character not found with Id {id}" });

            var characterDTO = _mapper.Map<CharacterDTO>(character);
            return Ok(characterDTO);
        }

        /// <summary>
        /// Adds a new character
        /// </summary>
        /// <param name="characterDTO">Character DTO</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> AddCharacter([FromBody] CharacterDTO characterDTO)
        {
            try
            {
                var character = _mapper.Map<Character>(characterDTO);
                var characterId = _repository.AddCharacter(character);
                var dto = new SuccessDTO
                {
                    Message = "Character added successfully",
                    Id = characterId
                };
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorDTO { Message = ex.Message });
            }

        }

        /// <summary>
        /// Updates an existing character
        /// </summary>
        /// <param name="characterDTO">Character DTO</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> UpdateCharacter([FromBody] CharacterDTO characterDTO)
        {
            try
            {
                var updatedCharacter = _mapper.Map<Character>(characterDTO);
                _repository.UpdateCharacter(updatedCharacter);
                return Ok(new SuccessDTO { Message = "Character updated successfully.", Id = characterDTO.Id });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundDTO { Message = $"Character not found with Id {characterDTO.Id}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorDTO { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes an existing character with the provided Id
        /// </summary>
        /// <param name="id">Character Id</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundDTO))]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            try
            {
                _repository.DeleteCharacter(id);
                return Ok(new SuccessDTO { Message = "Character deleted successfully.", Id = id });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundDTO { Message = $"Character not found with Id {id}" });
            }

        }
    }
}
