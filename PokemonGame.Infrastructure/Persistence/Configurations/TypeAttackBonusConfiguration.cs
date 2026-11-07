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
    public class TypeAttackBonusConfiguration : IEntityTypeConfiguration<TypeAttackBonus>
    {
        public void Configure(EntityTypeBuilder<TypeAttackBonus> builder)
        {
            builder.HasKey(x => new
            {
                x.DefenderTypeId,
                x.AttackTypeId
            });

            builder
                .HasOne(t => t.DefenderType)
                .WithMany(t => t.TypeDefendBonuses)
                .HasForeignKey(t => t.DefenderTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(t => t.AttackType)
                .WithMany(t => t.TypeAttackBonuses)
                .HasForeignKey(t => t.AttackTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
