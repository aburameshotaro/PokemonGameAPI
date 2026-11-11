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
    public class TmAttackConfiguration : IEntityTypeConfiguration<TmAttack>
    {
        public void Configure(EntityTypeBuilder<TmAttack> builder)
        {
            builder.HasKey(x => new
            {
                x.AttackId,
                x.PokemonSpeciesId
            });
        }
    }
}
