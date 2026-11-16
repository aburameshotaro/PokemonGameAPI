namespace PokemonGame.Api.Contracts.Trainers
{
    public sealed class CreateTrainerRequest
    {
        public string Username { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public int StarterSpeciesId { get; init; }
    }
}
