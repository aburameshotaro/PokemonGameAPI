using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Infrastructure.Persistence.Configurations
{
    public class ProbabilityAppearanceConfiguration : IEntityTypeConfiguration<ProbabilityAppearance>
    {
        public void Configure(EntityTypeBuilder<ProbabilityAppearance> builder)
        {
            builder.HasKey(x => new
            {
                x.PokemonSpeciesId,
                x.LocationId
            });
        }
    }
}
