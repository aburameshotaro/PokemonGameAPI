using Newtonsoft.Json.Linq;
using PokemonGame.Domain.Entities;
using PokemonGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Infrastructure.Persistence.Seeders
{
    public class LevelUpTMAttacksSeeder : Seeder
    {
        public readonly string _basePokemonUrl = @"https://pokeapi.co/api/v2/pokemon/";
        private IPokemonSpeciesRepository _pokemonSpeciesRepository;
        private IAttackRepository _attackRepository;
        private const string usedGameVersion = "firered-leafgreen";
        private enum LearningMethod {LevelUp, TM };

        public LevelUpTMAttacksSeeder(PokemonDbContext pokemonDb, IPokemonSpeciesRepository pokemonSpeciesRepository, IAttackRepository attackRepository) : base(pokemonDb)
        {
            _pokemonSpeciesRepository = pokemonSpeciesRepository;
            _attackRepository = attackRepository;
        }

        public async override Task Seed()
        {
            if (!_dbContext.LevelUpAttacks.Any())
            {
                var attacks = await GetPokemonAttacks<LevelUpAttack>(LearningMethod.LevelUp, 
                    (pokemonId, attackId, level) =>
                        new LevelUpAttack
                        {
                            PokemonSpeciesId = pokemonId,
                            AttackId = attackId,
                            Level = level ?? 0
                        });
                _dbContext.LevelUpAttacks.AddRange(attacks);
                await _dbContext.SaveChangesAsync();
            }
            if (!_dbContext.TMAttacks.Any())
            {
                var attacks = await GetPokemonAttacks<TmAttack>(LearningMethod.TM, 
                    (pokemonId, attackId, level) =>
                        new TmAttack
                        {
                            PokemonSpeciesId = pokemonId,
                            AttackId = attackId
                        });
                _dbContext.TMAttacks.AddRange(attacks);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task<IEnumerable<T>> GetPokemonAttacks<T>(LearningMethod learningMethod, Func<int, int, int?, T> factory)
        {
            List<T> attacks = new List<T>();
            List<PokemonSpecies> pokemonSpecies = await _pokemonSpeciesRepository.GetAllAsync();
            using HttpClient client = new();
            var attackLookup = (await _attackRepository.GetAllAsync())
                            .ToDictionary(
                            a => a.Name,
                            a => a.Id,
                            StringComparer.OrdinalIgnoreCase);

            foreach (PokemonSpecies pokemon in pokemonSpecies) 
            {
                var json = await client.GetStringAsync(_basePokemonUrl + pokemon.Name);
                JObject pokemonRemoteApi = JObject.Parse(json);
                foreach (var move in pokemonRemoteApi["moves"]!)
                {
                    var attackName = move["move"]!["name"]!.Value<string>()!;

                    string moveLearnMethod = learningMethod switch
                    {
                        LearningMethod.LevelUp => "level-up",
                        LearningMethod.TM => "machine",
                        _ => throw new ArgumentOutOfRangeException(nameof(learningMethod))
                    };


                    var learningDetails = move["version_group_details"]!.FirstOrDefault(
                        x => x["version_group"]!["name"]!.Value<string>()! == usedGameVersion &&
                        x["move_learn_method"]!["name"]!.Value<string>() == moveLearnMethod);

                    if (learningDetails == null)
                    {
                        continue;
                    }

                    if (!attackLookup.TryGetValue(attackName, out var attackId))
                    {
                        continue;
                    }

                    int? level = learningMethod == LearningMethod.LevelUp ? learningDetails["level_learned_at"]?.Value<int?>() : null;

                    attacks.Add(factory(pokemon.Id, attackId, level));
                }
            }
            return attacks;
        }
    }
}
