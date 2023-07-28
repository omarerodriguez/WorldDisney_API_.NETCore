using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MundoDeDisney.MundoDeDisney.Core.Entities.Configurations
{
    public class CharacterConfig : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {

            builder.Property(c => c.Name).HasMaxLength(100);
            builder.Property(c => c.Weigth).HasPrecision(3, 1);
        }
    }
}
