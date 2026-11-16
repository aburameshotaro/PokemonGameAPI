using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.DTOs
{
    public sealed class TrainerDto
    {
        public int Id { get; init; }

        public string Username { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public bool IsAdmin { get; init; }

        public bool IsBot { get; init; }
    }
}
