using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Construction_ASoldierWithAStrongerWeapenWinsFromSoldierWithWeakerWeapen()
        {
            var attacker = new Soldier("soldier1", Weapon.BareFist);
            var defender = new Soldier("soldier2", Weapon.Axe);

            var fight = new Fight(attacker, defender);
            var winner = fight.GetWinner();

            winner.Should().Be(defender);
        }

        [Fact]
        public void Construction_TwoSoldiersWithEqualWeaponsTheAttackerWins()
        {
            var attacker = new Soldier("soldier1", Weapon.BareFist);
            var defender = new Soldier("soldier2", Weapon.BareFist);

            var fight = new Fight(attacker, defender);
            var winner = fight.GetWinner();

            winner.Should().Be(attacker);
        }

        [Fact]
        public void Construction_GivenSoldiersWhenEnrollSoldiersThenSoldiersAreAddedToArmy()
        {
            var soldiers = new[]
            {
                new Soldier("soldier1", Weapon.BareFist),
                new Soldier("soldier2", Weapon.BareFist)
            };

            var army = new Army();
            foreach (var soldier in soldiers)
            {
                army.Enroll(soldier);
            }

            army.Soldiers.Should().BeEquivalentTo(soldiers);
        }

        [Fact]
        public void Construction_GivenAnArmyWhenEnrollingNullThenAnExceptionShouldBeThrown()
        {
            var army = new Army();

            Assert.Throws<ArgumentNullException>(() => army.Enroll(null));
        }

        [Fact]
        public void Construction_GivenAnArmyItShouldHaveAFrontMan()
        {
            var soldiers = new[]
            {
                new Soldier("soldier1", Weapon.BareFist),
                new Soldier("soldier2", Weapon.BareFist)
            };

            var army = new Army();
            foreach (var soldier in soldiers)
            {
                army.Enroll(soldier);
            }

            army.FrontMan.Should().BeEquivalentTo(soldiers[0]);
        }

        [Fact]
        public void Construction_GivenAnEmptyArmyRequestingFrontManShouldFail()
        {
            var army = new Army();

            Assert.Throws<EmptyArmyException>(() => army.FrontMan);
        }

        [Fact]
        public void Construction_GivenAnArmyWithoutSoldiers_HasSoldiersIsFalse()
        {
            var army = new Army();

            army.HasSoldiers.Should().BeFalse();
        }

        [Fact]
        public void Construction_GivenAnArmyWithSoldiers_HasSoldiersIsTrue()
        {
            var soldiers = new[]
            {
                new Soldier("soldier1", Weapon.BareFist),
                new Soldier("soldier2", Weapon.BareFist)
            };

            var army = new Army();
            foreach (var soldier in soldiers)
            {
                army.Enroll(soldier);
            }

            army.HasSoldiers.Should().BeTrue();
        }

        [Fact]
        public void Construction_GivenAnArmyWhenFrontManDiesHeIsRemovedFromArmy()
        {
            var soldiers = new[]
            {
                new Soldier("soldier1", Weapon.BareFist),
                new Soldier("soldier2", Weapon.BareFist)
            };

            var army = new Army();
            foreach (var soldier in soldiers)
            {
                army.Enroll(soldier);
            }

            army.FrontManDies();

            army.Soldiers.Should().BeEquivalentTo(new[] {soldiers[1]});
        }
    }
}