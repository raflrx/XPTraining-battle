using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FluentAssertions;
using Moq;
using Xunit;

namespace Battle.Tests
{
    public class SoldierTest
    {
        private Mock<IHeadQuarters> hqMock = new Mock<IHeadQuarters>();

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
        public void Construction_GivenSoldiersWhenEnrollSoldiersTheyAreEnlistedWithHq()
        {
            var soldiers = new[]
            {
                new Soldier("soldier1", Weapon.BareFist),
                new Soldier("soldier2", Weapon.BareFist)
            };

            var army = new Army(hqMock.Object);
            foreach (var soldier in soldiers)
            {
                army.Enroll(soldier);
            }

            hqMock.Verify(e => e.ReportEnlistment(It.Is<string>(name => soldiers.Select(s => s.Name).Contains(name))));
        }

        [Fact]
        public void Construction_GivenSoldiersWhenEnrollSoldiersTheSoldiersGetAnId()
        {
            var soldiers = new[]
            {
                new Soldier("soldier1", Weapon.BareFist),
                new Soldier("soldier2", Weapon.BareFist)
            };

            hqMock.Setup(x => x.ReportEnlistment(soldiers[0].Name)).Returns(1);
            hqMock.Setup(x => x.ReportEnlistment(soldiers[1].Name)).Returns(2);
            
            var army = new Army(hqMock.Object);
            foreach (var soldier in soldiers)
            {
                army.Enroll(soldier);
            }

            army.Soldiers[0].Id.Should().Be(1);
            army.Soldiers[1].Id.Should().Be(2);
        }

        [Fact]
        public void Construction_GivenSoldiersWhenEnrollSoldiersThenSoldiersAreAddedToArmy()
        {
            var soldiers = new[]
            {
                new Soldier("soldier1", Weapon.BareFist),
                new Soldier("soldier2", Weapon.BareFist)
            };

            var army = new Army(hqMock.Object);
            foreach (var soldier in soldiers)
            {
                army.Enroll(soldier);
            }

            
        }

        [Fact]
        public void Construction_GivenAnArmyWhenEnrollingNullThenAnExceptionShouldBeThrown()
        {
            var army = new Army(hqMock.Object);

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

            var army = new Army(hqMock.Object);
            foreach (var soldier in soldiers)
            {
                army.Enroll(soldier);
            }

            army.FrontMan.Should().BeEquivalentTo(soldiers[0]);
        }

        [Fact]
        public void Construction_GivenAnEmptyArmyRequestingFrontManShouldFail()
        {
            var army = new Army(hqMock.Object);

            Assert.Throws<EmptyArmyException>(() => army.FrontMan);
        }

        [Fact]
        public void Construction_GivenAnArmyWithoutSoldiers_HasSoldiersIsFalse()
        {
            var army = new Army(hqMock.Object);

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

            var army = new Army(hqMock.Object);
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

            var army = new Army(hqMock.Object);
            foreach (var soldier in soldiers)
            {
                army.Enroll(soldier);
            }

            army.FrontManDies();

            army.Soldiers.Should().BeEquivalentTo(new[] {soldiers[1]});
        }

        [Fact]
        public void Construction_GivenAnArmyWhenFrontManDiesThisIsReportedToHq()
        {
            var soldiers = new[]
            {
                new Soldier("soldier1", Weapon.BareFist),
                new Soldier("soldier2", Weapon.BareFist)
            };

            hqMock.Setup(x => x.ReportEnlistment(soldiers[0].Name)).Returns(1);
            hqMock.Setup(x => x.ReportEnlistment(soldiers[1].Name)).Returns(2);

            var army = new Army(hqMock.Object);
            foreach (var soldier in soldiers)
            {
                army.Enroll(soldier);
            }

            army.FrontManDies();

            hqMock.Verify(e => e.ReportCasualty(1), Times.Once);
        }

        [Fact]
        public void Construction_GivenAnArmyWinsNumberOfRemainingSoldiersIsReported()
        {
            // army1
            var soldiers1 = new[]
            {
                new Soldier("soldier1", Weapon.BareFist),
                new Soldier("soldier2", Weapon.Axe)
            };

            var attacker = new Army(hqMock.Object);
            foreach (var soldier in soldiers1)
            {
                attacker.Enroll(soldier);
            }

            // army2
            var soldiers2 = new[]
            {
                new Soldier("soldier1", Weapon.Sword),
                new Soldier("soldier2", Weapon.Spear),
                new Soldier("soldier3", Weapon.BareFist)
            };

            var defender = new Army(hqMock.Object);
            foreach (var soldier in soldiers2)
            {
                defender.Enroll(soldier);
            }

            var war = new War(attacker, defender, hqMock.Object);
            var winner = war.GetWinner();

            hqMock.Verify(x => x.ReportVictory(winner.Soldiers.Count), Times.Once);
        }

        [Fact]
        public void Construction_GivenEmptyArmyFrontManCanNotDie()
        {
            var army = new Army(hqMock.Object);
            Assert.Throws<EmptyArmyException>(() => army.FrontManDies());
        }

        [Fact]
        public void Construction_GivenTwoArmiesEngageInWarArmyWithLastManStandingWins()
        {
            // army1
            var soldiers1 = new[]
            {
                new Soldier("soldier1", Weapon.BareFist),
                new Soldier("soldier2", Weapon.Axe)
            };

            var attacker = new Army(hqMock.Object);
            foreach (var soldier in soldiers1)
            {
                attacker.Enroll(soldier);
            }

            // army2
            var soldiers2 = new[]
            {
                new Soldier("soldier1", Weapon.Sword),
                new Soldier("soldier2", Weapon.Spear),
                new Soldier("soldier3", Weapon.BareFist)
            };

            var defender = new Army(hqMock.Object);
            foreach (var soldier in soldiers2)
            {
                defender.Enroll(soldier);
            }

            var war = new War(attacker, defender, hqMock.Object);
            var winner = war.GetWinner();

            winner.Should().Be(attacker);
        }

        [Fact]
        public void Construction_givenOpponentWeaponHasEvenDamage_MagicPotionWins()
        {
            var soldierWithMagicPotion = new Soldier("soldier1", Weapon.MagicPotion);
            var otherSoldier = new Soldier("soldier2", Weapon.Spear);

            var fight = new Fight(soldierWithMagicPotion, otherSoldier);
            var winner = fight.GetWinner();

            winner.Should().Be(soldierWithMagicPotion);
        }

        [Fact]
        public void Construction_givenOpponentWeaponHasUnevenDamage_MagicPotionLooses()
        {
            var soldierWithMagicPotion = new Soldier("soldier1", Weapon.MagicPotion);
            var otherSoldier = new Soldier("soldier2", Weapon.Axe);

            var fight = new Fight(soldierWithMagicPotion, otherSoldier);
            var winner = fight.GetWinner();

            winner.Should().Be(otherSoldier);
        }

        [Fact]
        public void Construction_givenAttackerAndDefenderHaveMagicPotionAttackerShouldWin()
        {
            var attacker = new Soldier("soldier1", Weapon.MagicPotion);
            var defender = new Soldier("soldier2", Weapon.MagicPotion);

            var fight = new Fight(attacker, defender);
            var winner = fight.GetWinner();

            winner.Should().Be(attacker);
        }
    }
}