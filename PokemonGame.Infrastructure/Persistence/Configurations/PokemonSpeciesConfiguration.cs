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
    public class PokemonSpeciesConfiguration : IEntityTypeConfiguration<PokemonSpecies>
    {
        public void Configure(EntityTypeBuilder<PokemonSpecies> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder
                .HasOne(p => p.Type1)
                .WithMany(p => p.PokemonSpeciesType1)
                .HasForeignKey(p => p.Type1Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(p => p.Type2)
                .WithMany(p => p.PokemonSpeciesType2)
                .HasForeignKey(p => p.Type2Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(p => p.ProbabilityAppearances)
                .WithOne(p => p.PokemonSpecies)
                .HasForeignKey(p => p.PokemonSpeciesId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(p => p.LevelUpAttacks)
                .WithOne(p => p.PokemonSpecies)
                .HasForeignKey(p => p.PokemonSpeciesId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
