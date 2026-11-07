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
    public class CatchingItemConfiguration : IEntityTypeConfiguration<CatchingItem>
    {
        public void Configure(EntityTypeBuilder<CatchingItem> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
