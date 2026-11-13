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
    public class TypeAttackBonusesSeeder : Seeder
    {
        public readonly string _allTypesUrl = @"https://pokeapi.co/api/v2/type/";

        public TypeAttackBonusesSeeder(PokemonDbContext pokemonDb) : base(pokemonDb)
        {
        }

        public async override Task Seed()
        {
            if (!_dbContext.TypesAttackBonuses.Any())
            {
                var typeAttackBonuses = await GetTypeAttackBonuses();
                _dbContext.TypesAttackBonuses.AddRange(typeAttackBonuses);
                _dbContext.SaveChanges();
            }
        }

        private async Task<IEnumerable<TypeAttackBonus>> GetTypeAttackBonuses()
        {
            List<TypeAttackBonus> typeAttackBonuses = new();
            using HttpClient client = new();
            var json = await client.GetStringAsync(_allTypesUrl);
            JObject allTypes = JObject.Parse(json);
            foreach (var typeAttackBonus in allTypes["results"]!)
            {
                var typeUrl = typeAttackBonus["url"]!.Value<string>()!;
                var typeJson = await client.GetStringAsync(typeUrl);
                JObject typeData = JObject.Parse(typeJson);
                int attackTypeId = typeData["id"]!.Value<int>();
                JObject damageRelations = (JObject)typeData["damage_relations"]!;
                foreach (var relation in damageRelations)
                {
                    string relationName = relation.Key;
                    if(relationName.Contains("from")) continue;
                    double multiplier = relationName switch
                    {
                        "double_damage_to" => 2.0,
                        "half_damage_to" => 0.5,
                        "no_damage_to" => 0.0,
                        _ => 1.0
                    };
                    foreach (var defenderType in relation.Value!)
                    {
                        string defenderUrl = defenderType["url"]!.Value<string>()!;
                        int defenderTypeId = int.Parse(defenderUrl.TrimEnd('/').Split('/').Last());
                        typeAttackBonuses.Add(new TypeAttackBonus
                        {
                            Multiplier = (int)(multiplier * 100), // Store as integer percentage
                            AttackTypeId = attackTypeId,
                            DefenderTypeId = defenderTypeId
                        });
                    }
                }
            }

            return typeAttackBonuses;
        } 
    }
}
