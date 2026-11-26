using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGame.Domain.Entities
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsNpc { get; set; }
        public virtual List<Bag> Bags { get; set; } = new List<Bag>();
        public virtual List<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
        private Trainer()
        {
        }
        public Trainer(
        string username,
        string passwordHash,
        string email)
        {
            Username = username;
            PasswordHash = passwordHash;
            Email = email;
        }
        public int TeamSize => Pokemons.Count(p => p.NumberOnTeam != null);

        public void CatchPokemon(Pokemon pokemon)
        {
            pokemon.AssignToTrainer(Id);

            Pokemons.Add(pokemon);
        }

        public void AddPokemonToTeam(int pokemonId)
        {
            var pokemon = FindPokemon(pokemonId);

            if (pokemon.NumberOnTeam != null)
            {
                throw new InvalidOperationException("Pokemon is already in team.");
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
            var removedPosition = pokemon.NumberOnTeam.Value;
            pokemon.RemoveFromTeam();
            foreach (var teamPokemon in Pokemons.Where(p => p.NumberOnTeam > removedPosition))
            {
                teamPokemon.AssignToTeam(teamPokemon.NumberOnTeam!.Value - 1);
            }

        }

        public void ChangePokemonTeamPosition(int pokemonId, int targetPosition)
        {
            if (targetPosition < 1 || targetPosition > 6)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(targetPosition));
            }

            if (targetPosition > TeamSize)
            {
                throw new InvalidOperationException(
                    "Target position is not occupied.");
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

        private Pokemon FindPokemon(int pokemonId)
        {
            return Pokemons.FirstOrDefault(
                p => p.Id == pokemonId)
                ?? throw new InvalidOperationException(
                    "Pokemon does not belong to trainer.");
        }

    }
}
