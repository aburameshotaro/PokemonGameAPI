using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Tests.Common.Builders
{
    public class TrainerBuilder
    {
        private readonly Trainer _trainer = new("Ash", "Pikachu123", "email@gmail.com")
        {
            IsAdmin = false,
            IsNpc = false
        };

        public Trainer Build()
        {
            return _trainer;
        }

        public TrainerBuilder WithUsername(string username)
        {
            _trainer.Username = username;
            return this;
        }
    }
}
