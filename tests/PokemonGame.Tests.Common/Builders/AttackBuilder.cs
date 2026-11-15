using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Tests.Common.Builders
{
    public class AttackBuilder
    {
        private readonly Attack _attack = new()
        {
            Id = 1,
            Name = "Thunder Shock",
            Accuracy = 100,
            Power = 40,
            Priority = 0,
            DamageClass = "special",
            TypeId = 13
        };

        public Attack Build()
        {
            return _attack;
        }

        public AttackBuilder WithId(int id)
        {
            _attack.Id = id;
            return this;
        }

        public AttackBuilder WithName(string name)
        {
            _attack.Name = name;
            return this;
        }

        public AttackBuilder WithType(int typeId)
        {
            _attack.TypeId = typeId;
            return this;
        }
    }
}
