namespace MundoDeDisney.MundoDeDisney.Core.Entities
{
    public class Genre: BaseEntity
    {
        public string Name { get; set; } = null!;
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
