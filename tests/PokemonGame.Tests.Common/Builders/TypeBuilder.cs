using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Tests.Common.Builders
{
    public class TypeBuilder
    {
        private readonly Domain.Entities.Type _type = new()
        {
            Id = 13,
            Name = "electric"
        };

        public Domain.Entities.Type Build()
        {
            return _type;
        }

        public TypeBuilder WithId(int id)
        {
            _type.Id = id;
            return this;
        }

        public TypeBuilder WithName(string name)
        {
            _type.Name = name;
            return this;
        }
    }
}
