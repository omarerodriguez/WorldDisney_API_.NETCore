namespace MundoDeDisney.MundoDeDisney.Core.Entities
{
    public class Character: BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Weigth { get; set; }
        public string History { get; set; }
        public string Image { get; set; }
        public List<MovieCharacter> MoviesCharacters { get; set; } = new List<MovieCharacter>();
    }
}
