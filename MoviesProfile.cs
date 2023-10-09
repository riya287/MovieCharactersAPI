using AutoMapper;
using MovieAPI_Project.DTOs;
using MovieAPI_Project.Entity;

namespace MovieAPI_Project
{
    public class MoviesProfile:Profile
    {
        public MoviesProfile()
        {
            CreateMap<Movie, MovieDTO>();
            CreateMap<Character, CharacterDTO>();
            CreateMap<Franchise, FranchiseDTO>();

            CreateMap<MovieDTO, Movie>();
            CreateMap<CharacterDTO, Character>();
            CreateMap<FranchiseDTO, Franchise>();
        }

    }
}
