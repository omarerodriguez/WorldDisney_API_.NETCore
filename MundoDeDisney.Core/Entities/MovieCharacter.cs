﻿namespace MundoDeDisney.MundoDeDisney.Core.Entities
{
    public class MovieCharacter
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null;
        public int CharacterId { get; set; }
        public Character Character { get; set; }= null;
        public int Order { get; set; }
    }
}
