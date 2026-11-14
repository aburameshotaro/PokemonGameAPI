using PokemonGame.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.UnitTests.Domain
{
    public class PokemonClassTests
    {
        private static Pokemon CreatePokemon()
        {
            var species = CreateSpecies();
            var attack1 = new Attack() { Id = 1, Name = "Thunder Shock", Power = 40, Accuracy = 100, TypeId = 13 };
            var pokemon = new Pokemon(species, 10, attack1);
            return pokemon;
        }

        private static PokemonSpecies CreateSpecies()
        {
            return new PokemonSpecies()
            {
                Id = 25,
                Name = "Pikachu",
                BasicAttack = 55,
                BasicDefense = 40,
                BasicHp = 100,
                BasicSpecialAttack = 50,
                BasicSpecialDefense = 50,
                BasicSpeed = 90,
                Type1Id = 13,
                BasicExperienceGivenWhenDefeated = 100,
                Description = "Electric rat",
                Height = 100,
                Weight = 50,
                ExperienceSpeed = 2
            };
        }

        [Fact]
        public void Constructor_WithLevelLessThanOne_ThrowsException()
        {
            var species = CreateSpecies();
            var attack = new Attack();

            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Pokemon(species, 0, attack));
        }

        [Fact]
        public void Constructor_WithLevelGreaterThan100_ThrowsException()
        {
            var species = CreateSpecies();
            var attack = new Attack();

            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Pokemon(species, 101, attack));
        }

        [Fact]
        public void Constructor_WithNullAttack1_ThrowsException()
        {
            var species = CreateSpecies();

            Assert.Throws<ArgumentNullException>(
                () => new Pokemon(species, 10, null!));
        }

        [Theory]
        [InlineData(20, 10, 90)]
        [InlineData(50, 30, 80)]
        [InlineData(80, 50, 70)]
        [InlineData(33, 33, 100)]
        public void Heal_WithValidAmount_RestoresHp(int damageTaken, int amountHealed, int expectedHp)
        {
            // Arrange
            var pokemon = CreatePokemon();
            pokemon.TakeDamage(damageTaken);

            // Act
            pokemon.Heal(amountHealed);

            // Assert
            Assert.Equal(expectedHp, pokemon.CurrentHp);
        }

        [Theory]
        [InlineData(20, 30, 100)]
        [InlineData(50, 60, 100)]
        public void Heal_WhenHpWouldExceedMaxHp_CapsAtMaxHp(int damageTaken, int amountHealed, int expectedHp)
        {
            // Arrange
            var pokemon = CreatePokemon();
            pokemon.TakeDamage(damageTaken);

            // Act
            pokemon.Heal(amountHealed);

            // Assert
            Assert.Equal(expectedHp, pokemon.CurrentHp);
        }

        [Fact]
        public void Heal_WithNegativeAmount_ThrowsException()
        {
            // Arrange
            var pokemon = CreatePokemon();
            pokemon.TakeDamage(20);

            // Act
            Assert.Throws<ArgumentException>(
                () => pokemon.Heal(-10));
        }

        [Fact]
        public void Heal_WhenPokemonIsFainted_ThrowsException()
        {
            // Arrange
            var pokemon = CreatePokemon();
            pokemon.TakeDamage(100);

            // Act
            Assert.Throws<InvalidOperationException>(
                () => pokemon.Heal(100));
        }

        [Theory]
        [InlineData(20, 80)]
        [InlineData(50, 50)]
        [InlineData(100, 0)]
        public void TakeDamage_WithValidAmount_ReducesCurrentHp(int damage, int expectedHp)
        {
            // Arrange
            var pokemon = CreatePokemon();
            // Act
            pokemon.TakeDamage(damage);
            // Assert
            Assert.Equal(expectedHp, pokemon.CurrentHp);
        }

        [Fact]
        public void TakeDamage_WithNegativeAmount_ThrowsException()
        {
            // Arrange
            var pokemon = CreatePokemon();
            // Act
            Assert.Throws<ArgumentException>(
                () => pokemon.TakeDamage(-10));
        }

        [Fact]
        public void TakeDamage_WhenDamageExceedsCurrentHp_SetsCurrentHpToZero()
        {
            // Arrange
            var pokemon = CreatePokemon();
            // Act
            pokemon.TakeDamage(150);
            // Assert
            Assert.Equal(0, pokemon.CurrentHp);
        }

        [Fact]
        public void Revive_WhenPokemonIsNotFainted_ThrowsException()
        {
            // Arrange
            var pokemon = CreatePokemon();
            // Act
            Assert.Throws<InvalidOperationException>(
                () => pokemon.Revive(50));
        }

        [Fact]
        public void Revive_WhenPokemonIsFainted_SetsCurrentHpToRestoredAmount()
        {
            // Arrange
            var pokemon = CreatePokemon();
            pokemon.TakeDamage(100);
            // Act
            pokemon.Revive(50);
            // Assert
            Assert.Equal(50, pokemon.CurrentHp);
        }

        [Fact]
        public void Revive_WhenRestoredAmountExceedsMaxHp_CapsAtMaxHp()
        {
            // Arrange
            var pokemon = CreatePokemon();
            pokemon.TakeDamage(100);
            // Act
            pokemon.Revive(150);
            // Assert
            Assert.Equal(pokemon.MaxHp, pokemon.CurrentHp);
        }
    }
}
