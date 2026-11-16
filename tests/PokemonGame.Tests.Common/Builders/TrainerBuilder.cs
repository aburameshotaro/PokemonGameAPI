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
        private readonly Trainer _trainer = new()
        {
            Username = "Ash",
            PasswordHash = "Pikachu123",
            Email = "email@gmail.com",
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
