using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Infrastructure.Persistence.Configurations
{
    public class EvolutionConfiguration : IEntityTypeConfiguration<Evolution>
    {
        public void Configure(EntityTypeBuilder<Evolution> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(e => e.FromPokemonSpecies)
                .WithMany(e => e.EvolvesFrom)
                .HasForeignKey(e => e.FromPokemonSpeciesId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.ToPokemonSpecies)
                .WithMany(e => e.EvolvesInto)
                .HasForeignKey(e => e.ToPokemonSpeciesId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
