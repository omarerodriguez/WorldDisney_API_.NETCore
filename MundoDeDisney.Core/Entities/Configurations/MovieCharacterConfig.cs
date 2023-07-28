using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MundoDeDisney.MundoDeDisney.Core.Entities.Configurations
{
    public class MovieCharacterConfig : IEntityTypeConfiguration<MovieCharacter>
    {
        public void Configure(EntityTypeBuilder<MovieCharacter> builder)
        {
            builder.HasKey(mc => new {mc.MovieId,mc.CharacterId });
        }
    }
}
