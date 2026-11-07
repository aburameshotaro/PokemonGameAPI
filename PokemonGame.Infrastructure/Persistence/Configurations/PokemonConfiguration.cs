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
    public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
    {
        public void Configure(EntityTypeBuilder<Pokemon> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(p => p.Attack1)
                .WithMany(p => p.Pokemons1)
                .HasForeignKey(p => p.AttackId1)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(p => p.Attack2)
                .WithMany(p => p.Pokemons2)
                .HasForeignKey(p => p.AttackId2)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(p => p.Attack3)
                .WithMany(p => p.Pokemons3)
                .HasForeignKey(p => p.AttackId3)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(p => p.Attack4)
                .WithMany(p => p.Pokemons4)
                .HasForeignKey(p => p.AttackId4)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
