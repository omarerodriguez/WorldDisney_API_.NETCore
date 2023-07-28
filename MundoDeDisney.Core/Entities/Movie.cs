using System.ComponentModel.DataAnnotations;

namespace MundoDeDisney.MundoDeDisney.Core.Entities
{
    public class Movie: BaseEntity
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Rating { get; set; }
        public string Image { get; set; }
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<MovieCharacter> MoviesCharacters { get; set; } = new List<MovieCharacter>();
    }
}
