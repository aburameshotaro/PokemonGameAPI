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
    public class BagConfiguration : IEntityTypeConfiguration<Bag>
    {
        public void Configure(EntityTypeBuilder<Bag> builder)
        {
            builder.HasKey(x => new
            {
                x.TrainerId,
                x.ItemId
            });

            builder.HasOne(x => x.Trainer)
                   .WithMany(x => x.Bags)
                   .HasForeignKey(x => x.TrainerId);

            builder.HasOne(x => x.Item)
                   .WithMany(x => x.Bags)
                   .HasForeignKey(x => x.ItemId);
        }
    }
}
