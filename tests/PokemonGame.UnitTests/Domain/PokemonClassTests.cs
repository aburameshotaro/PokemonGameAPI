using PokemonGame.Domain.Entities;
using PokemonGame.Tests.Common.Builders;
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

        [Fact]
        public void Constructor_WithLevelLessThanOne_ThrowsException()
        {
            var species = new PokemonSpeciesBuilder().Build();
            var attack = new Attack();

            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Pokemon(species, 0, attack));
        }

        [Fact]
        public void Constructor_WithLevelGreaterThan100_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new PokemonBuilder()
                    .WithLevel(101)
                    .Build());
        }

        [Fact]
        public void Constructor_WithNullAttack1_ThrowsException()
        {
            var species = new PokemonSpeciesBuilder().Build();

            Assert.Throws<ArgumentNullException>(
                () => new PokemonBuilder()
                    .WithAttack(null)
                    .Build()); 
        }

        [Theory]
        [InlineData(20, 10, 90)]
        [InlineData(50, 30, 80)]
        [InlineData(80, 50, 70)]
        [InlineData(33, 33, 100)]
        public void Heal_WithValidAmount_RestoresHp(int damageTaken, int amountHealed, int expectedHp)
        {
            // Arrange
            var pokemon = new PokemonBuilder().Build();
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
            var pokemon = new PokemonBuilder().Build();
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
            var pokemon = new PokemonBuilder().Build();
            pokemon.TakeDamage(20);

            // Act
            Assert.Throws<ArgumentException>(
                () => pokemon.Heal(-10));
        }

        [Fact]
        public void Heal_WhenPokemonIsFainted_ThrowsException()
        {
            // Arrange
            var pokemon = new PokemonBuilder().Build();
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
            var pokemon = new PokemonBuilder().Build();
            // Act
            pokemon.TakeDamage(damage);
            // Assert
            Assert.Equal(expectedHp, pokemon.CurrentHp);
        }

        [Fact]
        public void TakeDamage_WithNegativeAmount_ThrowsException()
        {
            // Arrange
            var pokemon = new PokemonBuilder().Build();
            // Act
            Assert.Throws<ArgumentException>(
                () => pokemon.TakeDamage(-10));
        }

        [Fact]
        public void TakeDamage_WhenDamageExceedsCurrentHp_SetsCurrentHpToZero()
        {
            // Arrange
            var pokemon = new PokemonBuilder().Build();
            // Act
            pokemon.TakeDamage(150);
            // Assert
            Assert.Equal(0, pokemon.CurrentHp);
        }

        [Fact]
        public void Revive_WhenPokemonIsNotFainted_ThrowsException()
        {
            // Arrange
            var pokemon = new PokemonBuilder().Build();
            // Act
            Assert.Throws<InvalidOperationException>(
                () => pokemon.Revive(50));
        }

        [Fact]
        public void Revive_WhenPokemonIsFainted_SetsCurrentHpToRestoredAmount()
        {
            // Arrange
            var pokemon = new PokemonBuilder().Build();
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
            var pokemon = new PokemonBuilder().Build();
            pokemon.TakeDamage(100);
            // Act
            pokemon.Revive(150);
            // Assert
            Assert.Equal(pokemon.MaxHp, pokemon.CurrentHp);
        }
    }
}
