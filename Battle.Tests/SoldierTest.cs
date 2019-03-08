using System;
using FluentAssertions;
using Xunit;

namespace Battle.Tests
{
    public class SoldierTest
    {
        [Fact]
        public void Construction_ASoldierMustHaveAName()
        {
            var soldier = new Soldier("name");

            soldier.Name.Should().Be("name");
        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        public void Construction_ASoldierMustHaveAName_CannotBeBlank(string name)
        {
            Action act = () => new Soldier(name);
             
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Construction_ASoldierMustHaveExactlyOneWeapon()
        {
           var soldier = new Soldier("name");
           soldier.Weapon.Should().Be(Weapon.BareFist);
        }

        [Fact]
        public void Construction_ASoldierMustBeAbleToFightWithAnotherSoldier()
        {
            var soldier1 = new Soldier("soldier1", Weapon.BareFist);
            var soldier2 = new Soldier("soldier2", Weapon.Axe );

            var fight = new Fight(soldier1, soldier2);
            var winner = fight.GetWinner();

            winner.Should().Be(soldier1);
        }
    }

    public class Fight
    {
        private readonly Soldier _soldier1;
        private readonly Soldier _soldier2;

        public Fight(Soldier soldier1, Soldier soldier2)
        {
            _soldier1 = soldier1;
            _soldier2 = soldier2;
        }


        public Soldier GetWinner()
        {
            return null;
        }
    }
}