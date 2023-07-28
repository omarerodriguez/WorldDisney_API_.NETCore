using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Infrastructure.Validations;
using System.ComponentModel.DataAnnotations;

namespace MundoDeDisney.MundoDeDisney.Core.DTOs
{
    public class MovieDTO:BaseEntity
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Rating { get; set; }
    }
    public class MovieCreationDTO
    {
        [Required]
        [FirstLetterCapital]
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        public string Image { get; set; }
        public List<int> Genres { get; set; } = new List<int>();
        public List<MovieCharacterCreationDTO> MoviesCharacters { get; set; } = new List<MovieCharacterCreationDTO>();
    }
    public class MovieShowDetailsDTO:BaseEntity
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Rating { get; set; }
        public string Image { get; set; }
        public List<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
        public List<MovieCharacterDTO> Characters { get; set; } = new List<MovieCharacterDTO>();
    }
    public class MovieCharacterDTO
    {
        public int CharacterId { get; set; }
        public string Name { get; set; } = null;
    }

}
