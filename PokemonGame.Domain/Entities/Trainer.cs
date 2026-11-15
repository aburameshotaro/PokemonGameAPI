using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGame.Domain.Entities
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
        public bool Bot { get; set; }
        public virtual List<Bag> Bags { get; set; }
        public virtual List<Pokemon> Pokemons { get; set; }
        public int TeamSize => Pokemons.Count(p => p.NumberOnTeam != null);

        public void AddPokemonToTeam(int pokemonId)
        {
            var pokemon = FindPokemon(pokemonId);

            if (pokemon.NumberOnTeam != null)
            {
                throw new InvalidOperationException("Pokemon is already in team.");
            }

            if (pokemon.TrainerId != Id)
            {
                throw new InvalidOperationException("Pokemon doesn't belong to this trainer");
            }

            var freeSlot = TeamSize + 1;

            if (freeSlot > 6)
            {
                throw new InvalidOperationException("Team is full.");
            }

            pokemon.AssignToTeam(freeSlot);
        }

        public void RemovePokemonFromTeam(int pokemonId)
        {
            var pokemon = FindPokemon(pokemonId);

            if (pokemon.NumberOnTeam is null)
            {
                throw new InvalidOperationException(
                    "Pokemon is not in team.");
            }
            if (TeamSize <= 1)
            {
                throw new InvalidOperationException(
                    "Cannot remove the last Pokemon from the team.");
            }

            pokemon.RemoveFromTeam();
        }

        public void ChangePokemonTeamPosition(
        int pokemonId,
        int targetPosition)
        {
            if (targetPosition < 1 || targetPosition > 6)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(targetPosition));
            }

            var pokemon = FindPokemon(pokemonId);

            if (pokemon.NumberOnTeam is null)
            {
                throw new InvalidOperationException(
                    "Pokemon is not in team.");
            }

            var occupant = Pokemons.FirstOrDefault(
                p => p.NumberOnTeam == targetPosition);

            if (occupant is not null)
            {
                occupant.AssignToTeam(
                    pokemon.NumberOnTeam.Value);
            }

            pokemon.AssignToTeam(targetPosition);
        }

        public IReadOnlyCollection<Pokemon> GetTeam()
        {
            return Pokemons
                .Where(p => p.NumberOnTeam != null)
                .OrderBy(p => p.NumberOnTeam)
                .ToList();
        }

        public bool IsTeamFull()
        {
            return Pokemons.Count(p => p.NumberOnTeam != null) >= 6;
        }

        private Pokemon FindPokemon(int pokemonId)
        {
            return Pokemons.FirstOrDefault(
                p => p.Id == pokemonId)
                ?? throw new InvalidOperationException(
                    "Pokemon does not belong to trainer.");
        }

    }
}
