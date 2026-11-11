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
    public class LevelUpAttackConfiguration : IEntityTypeConfiguration<LevelUpAttack>
    {
        public void Configure(EntityTypeBuilder<LevelUpAttack> builder)
        {
            builder.HasKey(x => new
                {
                    x.PokemonSpeciesId,
                    x.AttackId
                });
        }
    }
}
