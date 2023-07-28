using AutoMapper;
using MundoDeDisney.MundoDeDisney.Core.DTOs;
using MundoDeDisney.MundoDeDisney.Core.Entities;

namespace MundoDeDisney.MundoDeDisney.Infrastructure.Mapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            //Gender Mapping
            CreateMap<GenreDTO, Genre>().ReverseMap();
            CreateMap<GenreShowDetailsDTO, Genre>().ReverseMap();
            CreateMap<GenreCreationDTO,Genre>().ReverseMap();
            CreateMap<GenrePutDTO, Genre>().ReverseMap();

            //Character Mapping
            CreateMap<CharacterDTO, Character>().ReverseMap();
            CreateMap<CharacterShowDetailsDTO, Character>().ReverseMap();
            CreateMap<CharacterCreationDTO, Character>().ReverseMap();
            CreateMap<CharacterPutDTO, Character>().ReverseMap();

            //Movie Mapping
            CreateMap<MovieDTO, Movie>().ReverseMap();
            CreateMap<MovieShowDetailsDTO, Movie>().ReverseMap();

            CreateMap<MovieCreationDTO, Movie>()
                .ForMember(ent => ent.Genres, dto =>
                dto.MapFrom(field => field.Genres.Select(id => new Genre { Id = id })));
            CreateMap<MovieCharacterCreationDTO, MovieCharacter>().ReverseMap();

            CreateMap<MovieShowDetailsDTO, MovieCharacter>().ReverseMap();

            CreateMap<UserDTO , User>().ReverseMap();
            CreateMap<UserLoginDTO, User>().ReverseMap();
        }
    }
}
