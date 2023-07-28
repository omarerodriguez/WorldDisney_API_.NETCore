using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Infrastructure.Validations;
using System.ComponentModel.DataAnnotations;

namespace MundoDeDisney.MundoDeDisney.Core.DTOs
{
    public class CharacterDTO:BaseEntity
    {
        public string Name { get; set; }
    }
    public class CharacterCreationDTO
    {
        [Required]
        [FirstLetterCapital]
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Weigth { get; set; }
        [Required]
        public string History { get; set; }
        public string Image { get; set; }
    }
    public class CharacterShowDetailsDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Weigth { get; set; }
        public string History { get; set; }
        public string Image { get; set; }
        public List<MovieDTO> Movies { get; set; } = new List<MovieDTO>();
    }
    public class CharacterPutDTO
    {
        public string Name { get; set; }
        [MaxLength(2)]
        public int Age { get; set; }
        public decimal Weigth { get; set; }
        [Required]
        public string History { get; set; }
        public string Image { get; set; }
        public List<CharacterMovieCreationDTO> CharactersMoviesCreationDTO { get; set; } = new List<CharacterMovieCreationDTO>();
    }
}
