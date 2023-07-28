using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Infrastructure.Validations;
using System.ComponentModel.DataAnnotations;

namespace MundoDeDisney.MundoDeDisney.Core.DTOs
{
    public class GenreDTO
    {
        public string Name { get; set; } = null!;
    }
    public class GenreCreationDTO
    {
        [Required]
        [FirstLetterCapital]
        public string Name { get; set; } = null!;
    }
    public class GenreShowDetailsDTO:BaseEntity
    {
        public string Name { get; set; } = null!;
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
    public class GenrePutDTO:BaseEntity
    {
        public string Name { get; set; } = null!;
    }
}
